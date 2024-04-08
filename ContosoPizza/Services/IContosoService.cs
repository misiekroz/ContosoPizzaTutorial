namespace ContosoPizza.Services
{
    /// <summary>
    /// This interface defines a common acess pattern to all current ContosoPizza services.
    /// NOTE: these services will at some point be replaced with EFCore
    /// </summary>
    public interface IContosoService<T>
    {
        /// <summary>
        /// Gets all available items from the list
        /// </summary>
        /// <returns>List of objects of type T</returns>
        public List<T> GetAll();

        /// <summary>
        /// Gets an item by ID
        /// </summary>
        /// <param name="id">ID to be looked for</param>
        /// <returns>Objetct if found, null if does not exist</returns>
        public T? Get(int id);

        /// <summary>
        /// Adds an item to the list, asigning the ID
        /// </summary>
        /// <param name="item">An object to be added</param>
        public void Add(T item);

        /// <summary>
        /// Updates an item
        /// </summary>
        /// <param name="item">Object to be updated</param>
        /// <returns>True if object found and updated by ID, else false</returns>
        public bool Update(T item);
    }
}
