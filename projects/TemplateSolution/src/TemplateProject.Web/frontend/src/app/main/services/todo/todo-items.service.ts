import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable, of } from 'rxjs';

import { TodoItem } from 'src/app/main/models/todo-item';
import { SortDirection } from 'src/app/shared/models/sort-direction';

@Injectable({
    providedIn: 'root'
})
export class TodoItemsService {
    constructor(private _http: HttpClient) {

    }

    getMany(searchTerm?: string, limit?: number, skip?: number, sortColumn?: string, sortDirection?: SortDirection): Observable<TodoItem[]> {
        let params = new HttpParams();
        if (searchTerm)
            params = params.append('searchTerm', searchTerm);
        if (limit !== undefined)
            params = params.append('limit', String(limit));
        if (skip !== undefined)
            params = params.append('skip', String(skip));
        if (sortColumn)
            params = params.append('sortColumn', sortColumn);
        if (sortDirection)
            params = params.append('sortDirection', sortDirection);

        return this._http.get<TodoItem[]>('/app/todo/index', {
            params: params
        });
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
