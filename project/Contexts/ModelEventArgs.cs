using System;

namespace Api.Contexts {
    public class ModelEventArgs<T> : EventArgs {
        public T Model { get; init; }
    }
}