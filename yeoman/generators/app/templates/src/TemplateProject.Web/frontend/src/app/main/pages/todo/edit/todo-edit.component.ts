import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormControl, Validators } from '@angular/forms';

import { DialogService } from 'src/app/shared/dialogs/dialog.service';
import { TodoItemsService } from 'src/app/main/services/todo/todo-items.service';
import { TodoItem } from 'src/app/main/models/todo-item';
import { asModelState } from 'src/app/shared/model-state/model-state';

@Component({
    selector: 'app-todo-edit',
    templateUrl: './todo-edit.component.html',
    styleUrls: ['./todo-edit.component.scss']
})
export class TodoEditComponent implements OnInit {
    private _isNew: boolean
    private _id: number;

    isLoading: boolean;
    nameControl: FormControl;
    itemForm: FormGroup;

    constructor(private _dialogService: DialogService,
        private _router: Router,
        private _todoItemsService: TodoItemsService,
        route: ActivatedRoute) {

        this.nameControl = new FormControl('', [ Validators.required ]);

        this.itemForm = new FormGroup({
            name: this.nameControl
        });

        route.params.subscribe(params => {
            this._isNew = params['id'] == null;

            if (!this._isNew) {
                let id = parseInt(params['id'], 10);
                if (isNaN(id)) {
                    this._dialogService.showErrorAsync({ text: 'Invalid item id' });

                    this._router.navigate(['/main/todo/index']);

                    return;
                }

                this._id = id;

                this.loadItem();
            }
        });
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

            this._router.navigate(['/main/todo/index']);
        }, error => {
            let errorText = 'A error occured while saving the item.';

            const modelState = asModelState(error);
            if (modelState != null)
                errorText = modelState.convertToString();

            this._dialogService.showErrorAsync({ text: errorText });

            this._router.navigate(['/main/todo/index']);
        });
    }

    private loadItem() {
        this.isLoading = true;

        this._todoItemsService.getById(this._id)
            .subscribe(item => {
                this.nameControl.setValue(item.name);

                this.isLoading = false;
            }, () => {
                this._dialogService.showErrorAsync({ text: 'A error occured while loading the item.' });

                this._router.navigate(['/main/todo/index']);
            });
    }
}
