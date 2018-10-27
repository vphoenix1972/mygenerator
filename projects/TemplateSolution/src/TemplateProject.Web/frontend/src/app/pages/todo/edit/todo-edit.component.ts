import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormControl } from '@angular/forms';

import { DialogService } from 'src/app/services/dialog/dialog.service';
import { TodoItemsService } from 'src/app/services/todo/todo-items.service';
import { TodoItem } from 'src/app/models/todo-item';

@Component({
    selector: 'app-todo-edit',
    templateUrl: './todo-edit.component.html',
    styleUrls: ['./todo-edit.component.scss']
})
export class TodoEditComponent implements OnInit {
    private _isNew: boolean
    private _id: number;

    isLoading: boolean;
    itemForm: FormGroup;

    constructor(private _dialogService: DialogService,
        private _router: Router,
        private _todoItemsService: TodoItemsService,
        route: ActivatedRoute) {

        this.itemForm = new FormGroup({
            name: new FormControl()
        });

        route.params.subscribe(params => {
            this._isNew = params['id'] == null;

            if (!this._isNew) {
                let id = parseInt(params['id'], 10);
                if (isNaN(id)) {
                    this._dialogService.showError({ text: 'Invalid item id' });

                    this._router.navigate(['/todo/index']);

                    return;
                }

                this._id = id;

                this.loadItem();
            }
        })
    }

    ngOnInit() {
    }

    onSubmit() {
        let item: TodoItem = new TodoItem();
        item.id = this._isNew ? null : this._id;
        item.name = this.itemForm.value.name;

        let saveObservable = this._isNew ?
            this._todoItemsService.add(item) :
            this._todoItemsService.update(this._id, item);

        saveObservable.subscribe(() => {
            this._dialogService.showSuccess();

            this._router.navigate(['/todo/index']);
        }, error => {
            this._dialogService.showError({ text: error.message });

            this._router.navigate(['/todo/index']);
        });
    }

    private loadItem() {
        this.isLoading = true;

        this._todoItemsService.getById(this._id)
            .subscribe(item => {
                this.itemForm.controls['name'].setValue(item.name);

                this.isLoading = false;
            }, error => {
                this._dialogService.showError({ text: error.message });

                this._router.navigate(['/todo/index']);
            });
    }
}
