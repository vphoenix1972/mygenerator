export class ExecutingDialogOptions {
    title: string = 'Executing...';
    onCancelAsync: () => Promise<void> = null;

    constructor(initializers?:Partial<ExecutingDialogOptions>) {
        Object.assign(this, initializers);
    }
}