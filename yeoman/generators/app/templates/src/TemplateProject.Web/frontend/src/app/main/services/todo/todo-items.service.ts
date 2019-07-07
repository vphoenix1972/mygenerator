import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';

import { TodoItem } from 'src/app/main/models/todo-item';

@Injectable({
    providedIn: 'root'
})
export class TodoItemsService {
    constructor(private _http: HttpClient) {

    }

    getAll(): Observable<TodoItem[]> {
        return this._http.get<TodoItem[]>('/app/todo/index');
    }

    getById(id: string): Observable<TodoItem> {
        return this._http.get<TodoItem>(`/app/todo/edit/${id}`);
    }

    add(item: TodoItem): Observable<TodoItem> {
        return this._http.post<TodoItem>('/app/todo', item);
    }

    update(id: string, item: TodoItem): Observable<any> {
        return this._http.put(`/app/todo/${id}`, item);
    }

    delete(id: string): Observable<any> {
        return this._http.delete(`/app/todo/${id}`);
    }
}
