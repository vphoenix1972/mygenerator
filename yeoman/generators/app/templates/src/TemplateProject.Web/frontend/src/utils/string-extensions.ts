import { isString } from './type-utils';

declare global {
    interface StringConstructor {
        isNullOrEmpty: (str: any) => boolean;
        isNullOrWhiteSpace: (str: any) => boolean;
    }
}

String.isNullOrEmpty = function (str: any): boolean {
    return !isString(str) || str.length < 1;
};

String.isNullOrWhiteSpace = function (str: any): boolean {
    return !isString(str) || str.trim().length < 1;
};