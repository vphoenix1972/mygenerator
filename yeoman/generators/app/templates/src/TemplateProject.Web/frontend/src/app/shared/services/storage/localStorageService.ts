import { Injectable } from '@angular/core';

@Injectable({
    providedIn: 'root'
})
export class LocalStorageService {
    getValue<T>(key: string): T {
        const valueJson = localStorage.getItem(key);

        if (valueJson == null)
            return null;

        let value: any;

        try {
            value = JSON.parse(valueJson);
        } catch (e) {
            console.log(`Error parsing '${key}' from local storage: ${e.name}:${e.message}\n${e.stack}`);

            return null;
        }

        return value;
    }

    setValue<T>(key: string, value: T): void {
        if (value == null) {
            this.removeValue(key);
            return;
        }

        const valueJson = JSON.stringify(value);

        localStorage.setItem(key, valueJson);
    }

    removeValue(key: string): void {
        localStorage.removeItem(key);
    }
}