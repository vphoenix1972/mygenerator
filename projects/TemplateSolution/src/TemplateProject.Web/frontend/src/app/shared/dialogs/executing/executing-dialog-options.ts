export class ExecutingDialogOptions {
    title: string = 'Executing...';
    onCancelAsync: () => Promise<boolean> = null;

    constructor(initializers?:Partial<ExecutingDialogOptions>) {
        Object.assign(this, initializers);
    }
}