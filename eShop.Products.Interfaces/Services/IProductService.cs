namespace eShop.Products.Interfaces.Services;

/// <summary>
/// Админка каталога - Управление товарами и их параметрами
/// </summary>
public interface IProductService
{
    /// <summary>
    /// Получение дерева товарных подгрупп
    /// </summary>
    void GetProductGroupsTree();
    /// <summary>
    /// Получение списка типов товаров в товарной подгруппе
    /// </summary>
    void GetProductGroupTypes(Guid ProductGroupId);
    /// <summary>
    /// Получение списка товаров по типу
    /// </summary>
    void GetProductTypeProducts(Guid ProductTypeId);

    /// <summary>
    /// получение товара с параметрами
    /// </summary>
    void GetProduct(Guid ProductId);

    /// <summary>
    /// Список групп параметров
    /// </summary>
    void GetParamGroups();
    /// <summary>
    /// Список параметров в группе параметров
    /// </summary>
    void GetParamGroupParams(int ParamGroupId);
    /// <summary>
    /// Получение параметра со значениями
    /// </summary>
    void GetParam(int ParamId);
    /// <summary>
    /// Получение списка параметров от типа продуктов
    /// </summary>
    void GetProductTypeParams(Guid ProductTypeId);



    Guid AddProductGroup(Guid ParentProductGroupId, string name, string description);
    void AttachProductGroup(Guid ProductGroupId, Guid ParentProductGroupId);
    void UpdateProductGroup(Guid ProductGroupId,  string name, string description);
    void SoftDeleteProductGroup(Guid ProductGroupId);


    Guid AddProductType(Guid ProductGroupId, string name, string description);
    void AttachProductType(Guid ProductTypeId, Guid ProductGroupId);
    void UpdateProductType(Guid ProductTypeId, string name, string description);
    void SoftDeleteProductType(Guid ProductTypeId);

    void AddProductTypeParam(Guid ProductTypeId, int ParamId);
    void DeleteProductTypeParam(Guid ProductTypeId, int ParamId);


    Guid AddProduct(Guid ProductTypeId, string name, string description);
    void AttachProduct(Guid ProductTypeId, Guid ProductId);
    void UpdateProduct(Guid ProductId, string name, string description);
    void SoftDeleteProduct(Guid ProductId);
    void AddProductParamValue(Guid ProductId, int ParamId, string value);
    void DeleteProductParamValue(Guid ProductId, int ParamId, string value);


    int AddParamGroup(string name, string description);
    void UpdateParamGroup(int ParamGroupId, string name, string description);

    int AddParam(int ParamGroupId, string name, string description);
    int AttachParam(int ParamId, int ParamGroupId);
    void UpdateParam(int ParamId, string name, string description);
    void DeleteParam(int ParamId);
    void AddParamValue(int ParamId, string value);
    void DeleteParamValue(int ParamId, string value);


}
