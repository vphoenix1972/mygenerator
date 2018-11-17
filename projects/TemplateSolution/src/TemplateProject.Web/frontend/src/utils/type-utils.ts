export function isObject(value: any): boolean {
    return value !== null && typeof value === 'object' && !isArray(value);
}

export function isString(value: any): boolean {
    return typeof value === 'string';
}

export function isNumber(value) {
    return typeof value === 'number';
}

export function isArray(value: any): boolean {
    return Array.isArray(value);
}

export function isFunction(value) {
    return typeof value === 'function';
}

export function isBoolean(value) {
    return typeof value === 'boolean';
}