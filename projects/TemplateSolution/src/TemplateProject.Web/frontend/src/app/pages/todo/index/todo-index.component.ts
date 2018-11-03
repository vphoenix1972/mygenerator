import { Component, OnInit } from '@angular/core';

import { TodoItemsService } from 'src/app/services/todo/todo-items.service';
import { DialogService } from 'src/app/shared/dialogs/dialog.service';

@Component({
    selector: 'app-todo-index',
    templateUrl: './todo-index.component.html',
    styleUrls: ['./todo-index.component.scss']
})
export class TodoIndexComponent implements OnInit {
    isLoading: boolean;
    items: Array<any>;

    constructor(private _todoItemsService: TodoItemsService,
        private _dialogService: DialogService) { }

    ngOnInit() {
        this.isLoading = true;

        this._todoItemsService.getAll()
            .subscribe(items => {
                this.items = items;

                this.isLoading = false;
            });
    }

    onDeleteButtonClicked(item: any): boolean {
        this._todoItemsService.delete(item.id)
            .subscribe(() => {
                this.items.splice(this.items.indexOf(item), 1);

                this._dialogService.showSuccess();
            });

        return false;
    }
}
