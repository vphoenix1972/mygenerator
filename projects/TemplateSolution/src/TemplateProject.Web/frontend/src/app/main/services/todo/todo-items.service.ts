import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable, of } from 'rxjs';

import { TodoItem } from 'src/app/main/models/todo-item';
import { SortDirection } from 'src/app/shared/models/sort-direction';

export interface GetItemsResponse {
    items: TodoItem[];
    total: number;
}

@Injectable({
    providedIn: 'root'
})
export class TodoItemsService {
    constructor(private _http: HttpClient) {

    }

    getMany(nameFilter?: string, limit?: number, skip?: number, sortColumn?: string, sortDirection?: SortDirection): Observable<GetItemsResponse> {
        let params = new HttpParams();
        if (nameFilter)
            params = params.append('nameFilter', nameFilter);
        if (limit !== undefined)
            params = params.append('limit', String(limit));
        if (skip !== undefined)
            params = params.append('skip', String(skip));
        if (sortColumn)
            params = params.append('sortColumn', sortColumn);
        if (sortDirection)
            params = params.append('sortDirection', sortDirection);

        return this._http.get<GetItemsResponse>('/api/app/todoItems', {
            params: params
        });
    }

    getById(id: string): Observable<TodoItem> {
        return this._http.get<TodoItem>(`/api/app/todoItems/${id}`);
    }

    add(item: TodoItem): Observable<TodoItem> {
        return this._http.post<TodoItem>('/api/app/todoItems', item);
    }

    update(id: string, item: TodoItem): Observable<any> {
        return this._http.put(`/api/app/todoItems/${id}`, item);
    }

    delete(id: string): Observable<any> {
        return this._http.delete(`/api/app/todoItems/${id}`);
    }
}
