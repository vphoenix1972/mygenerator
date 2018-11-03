export class ErrorDialogOptions {
    title: string = 'Error';
    text: string = 'A error occured.';

    constructor(initializers?:Partial<ErrorDialogOptions>) {
        Object.assign(this, initializers);
    }
}