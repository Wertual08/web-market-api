using System;

namespace Api.Database {
    public class ModelEventArgs<T> : EventArgs {
        public T Model { get; init; }
    }
}