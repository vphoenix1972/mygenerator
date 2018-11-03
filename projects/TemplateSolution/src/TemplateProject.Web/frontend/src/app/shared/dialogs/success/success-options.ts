export class SuccessOptions {
    title: string = 'Success';
    text: string = 'Operation has completed successfully.';

    constructor(initializers?:Partial<SuccessOptions>) {
        Object.assign(this, initializers);
    }
}