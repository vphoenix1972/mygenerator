import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';

import { TodoItem } from 'src/app/models/todo-item';

@Injectable({
    providedIn: 'root'
})
export class TodoItemsService {
    private _items: TodoItem[];
    private _index: number;

    constructor() {
        this._items = [
            { id: 1, name: "Item 1" },
            { id: 2, name: "Item 2" },
            { id: 3, name: "Item 3" }
        ];

        this._index = 4;
    }

    getAll(): Observable<TodoItem[]> {
        return of(this._items.slice(0));
    }

    add(item: TodoItem): TodoItem {
        item.id = this._index;
        item.name = `Item ${this._index}`;
        
        this._items.push(item);

        this._index++;

        return item;
    }

    delete(item: TodoItem) {
        this._items.splice(this._items.indexOf(item), 1);
    }
}
