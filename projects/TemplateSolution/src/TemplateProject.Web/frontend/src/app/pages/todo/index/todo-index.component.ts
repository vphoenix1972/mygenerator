import { Component, OnInit } from '@angular/core';

import { TodoItemsService } from 'src/app/services/todo-items.service';
import { TodoItem } from 'src/app/models/todo-item';

@Component({
    selector: 'app-todo-index',
    templateUrl: './todo-index.component.html',
    styleUrls: ['./todo-index.component.scss']
})
export class TodoIndexComponent implements OnInit {
    isLoading: boolean;
    items: Array<any>;

    constructor(private _todoItemsService: TodoItemsService) { }

    ngOnInit() {
        this.isLoading = true;

        this._todoItemsService.getAll()
            .subscribe(items => {
                this.items = items

                this.isLoading = false;
            });
    }

    onAddButtonClicked(): boolean {
        let newItem = new TodoItem();

        newItem = this._todoItemsService.add(newItem);

        this.items.push(newItem);

        return false;
    }

    onEditButtonClicked(item: any): boolean {
        console.log(`Edit item: ${item}`);

        return false;
    }

    onDeleteButtonClicked(item: any): boolean {
        this.items.splice(this.items.indexOf(item), 1);

        this._todoItemsService.delete(item);

        return false;
    }
}
