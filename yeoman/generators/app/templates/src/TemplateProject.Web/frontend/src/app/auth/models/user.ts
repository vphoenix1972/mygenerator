import { Role } from './role';

export class User {
    readonly isAuthenticated: boolean;
    readonly id: string;
    readonly name: string;
    readonly roles: Role[];

    constructor(initializers?: Partial<User>) {
        Object.assign(this, initializers);
    }
}