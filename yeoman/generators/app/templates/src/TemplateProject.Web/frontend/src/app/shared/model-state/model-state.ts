import { HttpErrorResponse } from "@angular/common/http";
import { isObject, isArray, isString } from "src/utils/type-utils";


export class ModelState {
    convertToString(options: { errorDelimeter: string } = { errorDelimeter: '\r\n' }): string {
        let errors: string[] = [];

        for (const key in this) {
            let value: any = this[key];
            if (!isArray(value))
                continue;

            value = value.filter(e => isString(e));

            errors = errors.concat(value);
        }

        return errors.join(options.errorDelimeter);
    }
}

export function asModelState(error: any): ModelState {
    if (error instanceof HttpErrorResponse) {
        const httpErrorResponse = error as HttpErrorResponse;
        if (httpErrorResponse.status === 400 && isObject(httpErrorResponse.error)) {
            const modelState = new ModelState();

            Object.assign(modelState, httpErrorResponse.error);

            return modelState;
        }
    }

    return null;
}