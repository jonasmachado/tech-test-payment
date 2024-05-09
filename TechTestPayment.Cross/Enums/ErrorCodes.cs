using System.ComponentModel;

namespace TechTestPayment.Cross.Enums
{
    /// <summary>
    /// Represents the set of errors that the application is ready to handle.
    /// </summary>
    public enum ErrorCodes
    {
        /// <summary>
        /// A non specific error.
        /// </summary>
        [Description("[POT-000] An unspecified problem occurred.")]
        Generic = 0,

        /// <summary>
        /// The information provided is not valid.
        /// </summary>
        [Description("[POT-001] Request data is invalid.")]
        InvalidRequestData = 1,

        /// <summary>
        /// Null object could not be modified.
        /// </summary>
        [Description("[POT-002] An attempt to modify a null entity was made.")]
        ModifyNullEntityAttempt = 2,

        /// <summary>
        /// Could not find order.
        /// </summary>
        [Description("[POT-003] Order/Sale not found.")]
        OrderNotFound = 3,

        /// <summary>
        /// Status not allowed.
        /// </summary>
        [Description("[POT-004] Not allowed to change from status [{0}] to requested status [{1}].")]
        StatusNotAllowed = 4,

        /// <summary>
        /// There is already a registered cpf.
        /// </summary>
        [Description("[POT-005] The seller already exists.")]
        SellerAlreadyExists = 5,

        /// <summary>
        /// Seller does not exists.
        /// </summary>
        [Description("[POT-006] The seller does not exist.")]
        SellerDoesNotExist = 6,

        /// <summary>
        /// Product already exists
        /// </summary>
        [Description("[POT-007] The product already exists.")]
        ProductAlreadyExists = 7,

        /// <summary>
        /// No stock available.
        /// </summary>
        [Description("[POT-008] The stock is insufficient for the order.")]
        InsufficientStock = 8,

        /// <summary>
        /// Product does not exist.
        /// </summary>
        [Description("[POT-009] The product does not exist.")]
        ProductDoesNotExist = 9
    }
}
