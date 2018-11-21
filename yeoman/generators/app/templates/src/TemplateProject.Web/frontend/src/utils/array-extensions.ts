interface Array<T> {
    any(this: Array<T>, predicate?: (T) => boolean): boolean;
    all(this: Array<T>, predicate?: (T) => boolean): boolean;
}

Array.prototype.any = function (predicate?: (T) => boolean) {
    /// <summary>
    /// Determines whether any element of a sequence satisfies a condition.
    /// </summary>

    const self = this;

    if (predicate) {
        for (let i = 0; i < self.length; i++) {
            const item = self[i];
            if (predicate(item))
                return true;
        }

        return false;
    }

    return self.length > 0;
};

Array.prototype.all = function (predicate?: (T) => boolean) {
    /// <summary>
    /// Determines whether any element of a sequence satisfies a condition.
    /// </summary>

    const self = this;

    if (predicate == null)
        throw new Error('Argument "predicate" cannot be null.');

    for (let i = 0; i < self.length; i++) {
        const item = self[i];
        if (!predicate(item))
            return false;
    }

    return true;
};