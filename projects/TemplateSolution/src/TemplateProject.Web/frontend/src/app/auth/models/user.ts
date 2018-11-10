import { Roles } from './roles';

export class User {
    readonly isAuthenticated: boolean;
    readonly id: number;
    readonly name: string;
    readonly roles: Roles[];

    constructor(initializers?: Partial<User>) {
        Object.assign(this, initializers);
    }
}