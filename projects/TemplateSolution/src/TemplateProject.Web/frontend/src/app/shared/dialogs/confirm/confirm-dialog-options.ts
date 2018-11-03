export class ConfirmDialogOptions {
    title: string = 'Are you sure?';
    yesButtonText: string = 'Yes';
    noButtonText: string = 'No';

    constructor(initializers?:Partial<ConfirmDialogOptions>) {
        Object.assign(this, initializers);
    }
}