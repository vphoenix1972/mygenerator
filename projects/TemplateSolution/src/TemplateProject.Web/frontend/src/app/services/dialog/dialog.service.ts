import { Injectable } from '@angular/core';

@Injectable({
    providedIn: 'root'
})
export class DialogService {

    constructor() { }

    showSuccess(options: any = null) {
        let defaultOptions = {
            text: 'Operation has completed successfully.'
        };

        if (options != null && typeof options === 'object') {
            if (options.text == null)
                options.text = defaultOptions.text;
        } else {
            options = defaultOptions;
        }

        alert(options.text);
    }

    showError(options: any) {
        alert(options.text);
    }
}
