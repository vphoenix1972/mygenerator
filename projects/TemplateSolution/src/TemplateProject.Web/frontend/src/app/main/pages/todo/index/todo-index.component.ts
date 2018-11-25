import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { TodoItemsService } from 'src/app/main/services/todo/todo-items.service';
import { DialogService } from 'src/app/shared/dialogs/dialog.service';
import { ConfirmResult } from 'src/app/shared/dialogs/confirm/confirm-result';

@Component({
    selector: 'app-todo-index',
    templateUrl: './todo-index.component.html',
    styleUrls: ['./todo-index.component.scss']
})
export class TodoIndexComponent implements OnInit {
    isLoading: boolean;
    items: Array<any>;

    constructor(private _todoItemsService: TodoItemsService,
        private _dialogService: DialogService,
        private _router: Router) { }

    ngOnInit() {
        this.isLoading = true;

        this._todoItemsService.getAll()
            .subscribe(items => {
                this.items = items;

                this.isLoading = false;
            }, error => {
                this._dialogService.showErrorAsync({ text: error.message })
                    .then(() => this._router.navigate(['/main']));
            });
    }

    onDeleteButtonClicked(item: any): boolean {
        this._dialogService.showConfirmAsync({ title: `Are you sure to delete user '${item.name}'?` })
            .then(result => {
                if (result != ConfirmResult.Yes)
                    return;

                this._dialogService.showExecuting();

                this._todoItemsService.delete(item.id)
                    .subscribe(() => {
                        this.items.splice(this.items.indexOf(item), 1);

                        this._dialogService.showSuccess();
                    }, () => this._dialogService.showErrorAsync({ text: 'A error occured while deleting the item.' }));
            });

        return false;
    }
}
