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
    void GetProductGroupTypes(int ProductGroupId);
    /// <summary>
    /// Получение списка товаров по типу
    /// </summary>
    void GetProductTypeProducts(int ProductTypeId);

    /// <summary>
    /// получение товара с параметрами
    /// </summary>
    void GetProduct(int ProductId);

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
    void GetProductTypeParams(int ProductTypeId);



    Guid AddProductGroup(int ParentProductGroupId, string name, string description);
    void AttachProductGroup(int ProductGroupId, int ParentProductGroupId);
    void UpdateProductGroup(int ProductGroupId,  string name, string description);
    void SoftDeleteProductGroup(int ProductGroupId);


    int AddProductType(int ProductGroupId, string name, string description);
    void AttachProductType(int ProductTypeId, int ProductGroupId);
    void UpdateProductType(int ProductTypeId, string name, string description);
    void SoftDeleteProductType(int ProductTypeId);

    void AddProductTypeParam(int ProductTypeId, int ParamId);
    void DeleteProductTypeParam(int ProductTypeId, int ParamId);


    int AddProduct(int ProductTypeId, string name, string description);
    void AttachProduct(int ProductTypeId, int ProductId);
    void UpdateProduct(int ProductId, string name, string description);
    void SoftDeleteProduct(int ProductId);
    void AddProductParamValue(int ProductId, int ParamId, string value);
    void DeleteProductParamValue(int ProductId, int ParamId, string value);


    int AddParamGroup(string name, string description);
    void UpdateParamGroup(int ParamGroupId, string name, string description);

    int AddParam(int ParamGroupId, string name, string description);
    int AttachParam(int ParamId, int ParamGroupId);
    void UpdateParam(int ParamId, string name, string description);
    void DeleteParam(int ParamId);
    void AddParamValue(int ParamId, string value);
    void DeleteParamValue(int ParamId, string value);


}
