import { Component, QueryList, ViewChildren } from '@angular/core';

import { GetItemsResponse, TodoItemsService } from 'src/app/main/services/todo/todo-items.service';
import { DialogService } from 'src/app/shared/dialogs/dialog.service';
import { ConfirmResult } from 'src/app/shared/dialogs/confirm/confirm-result';
import { TodoItem } from "../../../models/todo-item";
import { BehaviorSubject, Observable, Subject, from } from "rxjs";
import { debounceTime, delay, map, switchMap, tap, catchError } from "rxjs/operators";

import { SortableHeaderDirective, SortEvent } from 'src/app/shared/directives/sortable-header/sortable-header.directive';
import { SortDirection } from 'src/app/shared/models/sort-direction';

@Component({
    selector: 'app-todo-index',
    templateUrl: './todo-index.component.html',
    styleUrls: ['./todo-index.component.scss']
})
export class TodoIndexComponent {
    private _isLoading$ = new BehaviorSubject<boolean>(true);
    private _items$ = new BehaviorSubject<TodoItem[]>([]);
    private _search$ = new Subject<void>();
    private _total$ = new BehaviorSubject<number>(0);
    private _page: number;
    private _pageSize: number;
    private _nameFilter: string;
    private _sortColumn: string;
    private _sortDirection: SortDirection;

    @ViewChildren(SortableHeaderDirective) headers: QueryList<SortableHeaderDirective>;

    constructor(private _todoItemsService: TodoItemsService,
        private _dialogService: DialogService) {

        this._page = 1;
        this._pageSize = 10;

        this._search$.pipe(
            tap(() => this._isLoading$.next(true)),
            debounceTime(200),
            switchMap(() => this._search()),
            delay(200),
            tap(() => this._isLoading$.next(false))
        ).subscribe(result => {
            this._items$.next(result.items);
            this._total$.next(result.total);
        }, error => {
            this._dialogService.showErrorAsync({ text: error.message })
                    .then(() => this._isLoading$.next(false));
        });

        this._search$.next();
    }

    get isLoading$() { return this._isLoading$.asObservable(); }
    get items$() { return this._items$.asObservable(); }
    get total$() { return this._total$.asObservable(); }

    get page() { return this._page; }
    set page(value: number) {
        this._page = value;
        this._search$.next();
    }

    get pageSize() { return this._pageSize; }
    set pageSize(value: number) {
        this._pageSize = value;
        this._search$.next();
    }

    get nameFilter() { return this._nameFilter; }
    set nameFilter(value: string) {
        this._nameFilter = value;
        this._search$.next();
    }

    onDeleteButtonClicked(item: any): boolean {
        this._dialogService.showConfirmAsync({ title: `Are you sure to delete user '${item.name}'?` })
            .then(result => {
                if (result != ConfirmResult.Yes)
                    return;

                this._dialogService.showExecuting();

                this._todoItemsService.delete(item.id)
                    .subscribe(() => {
                        this._search$.next();

                        this._dialogService.showSuccess();
                    }, () => this._dialogService.showErrorAsync({ text: 'A error occured while deleting the item.' }));
            });

        return false;
    }

    onSort({ column, direction }: SortEvent) {
        // resetting other headers
        this.headers.forEach(header => {
            if (header.sortable !== column) {
                header.direction = '';
            }
        });

        this._sortColumn = direction ? column : '';
        this._sortDirection = direction;

        this._search$.next();
    }

    private _search(): Observable<GetItemsResponse> {
        return this._todoItemsService.getMany(
            this._nameFilter,
            this._pageSize,
            (this._page - 1) * this._pageSize,
            this._sortColumn,
            this._sortDirection
        ).pipe(
                catchError(error =>
                    from(this._dialogService.showErrorAsync({ text: error.message })
                            .then(() => ({ items: [], total: 0 }))
                    ))
        );
    }
}