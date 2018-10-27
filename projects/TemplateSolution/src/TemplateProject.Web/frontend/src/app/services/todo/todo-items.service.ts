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

    getById(id: number): Observable<TodoItem> {
        return Observable.create(observer => {
            let item: TodoItem = this._items.find(e => e.id == id);

            if (item == null)
                throw new Error(`Item '${id}' was not found.`);

            observer.next(item);
        });
    }

    add(item: TodoItem): Observable<TodoItem> {
        return Observable.create(observer => {
            item.id = this._index;

            this._items.push(item);

            this._index++;

            observer.next(item);
        });
    }

    update(id: number, item: TodoItem): Observable<any> {
        return Observable.create(observer => {
            const existng = this._items.find(e => e.id == id);
            if (existng == null)
                throw new Error(`Item '${id}' was not found.`);

            existng.name = item.name;

            observer.next();
        });
    }

    delete(item: TodoItem): Observable<any> {
        return Observable.create(observer => {
            this._items.splice(this._items.indexOf(item), 1);

            observer.next();
        });
    }
}
