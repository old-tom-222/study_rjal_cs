using System;
using System.Collections.Generic;
using CSproject.Business.Models;
using CSproject.Data.Repositories;

namespace CSproject.Business.Services
{
    public class ProductService
    {
        private readonly ProductRepository _productRepo = new ProductRepository();
        private readonly ProductCategoryRepository _categoryRepo = new ProductCategoryRepository();

        /// <summary>
        /// 获取商品列表
        /// </summary>
        public List<Product> GetProducts(string productName = null, int? categoryId = null, string status = null)
        {
            return _productRepo.GetProducts(productName, categoryId, status);
        }

        /// <summary>
        /// 获取所有商品列表（用于下拉框）
        /// </summary>
        public List<Product> GetAllProducts()
        {
            return _productRepo.GetAllProducts();
        }

        /// <summary>
        /// 获取商品详情
        /// </summary>
        public Product GetProductById(int productId)
        {
            return _productRepo.GetProductById(productId);
        }

        /// <summary>
        /// 获取活跃商品列表
        /// </summary>
        public List<Product> GetActiveProducts()
        {
            return _productRepo.GetActiveProducts();
        }

        /// <summary>
        /// 创建商品
        /// </summary>
        public int CreateProduct(Product product)
        {
            try
            {
                // 业务逻辑验证
                if (string.IsNullOrWhiteSpace(product.ProductName))
                {
                    throw new ArgumentException("商品名称不能为空");
                }

                if (string.IsNullOrWhiteSpace(product.Sku))
                {
                    throw new ArgumentException("商品SKU不能为空");
                }

                // 检查分类是否存在
                var category = _categoryRepo.GetProductCategoryById(product.CategoryId);
                if (category == null)
                {
                    throw new ArgumentException("无效的商品分类");
                }

                // 验证价格
                if (product.CostPrice < 0)
                {
                    throw new ArgumentException("成本价格不能小于0");
                }

                if (product.SalePrice < 0)
                {
                    throw new ArgumentException("销售价格不能小于0");
                }

                if (product.SalePrice < product.CostPrice)
                {
                    throw new ArgumentException("销售价格不能低于成本价格");
                }

                // 验证安全库存
                if (product.SafeStock < 0)
                {
                    throw new ArgumentException("安全库存不能小于0");
                }

                // 设置默认状态
                if (string.IsNullOrWhiteSpace(product.Status))
                {
                    product.Status = "1"; // 活跃
                }

                // 设置日期
                product.CreatedDate = DateTime.Now;
                product.LastUpdated = DateTime.Now;

                // 调用数据访问层创建商品
                return _productRepo.CreateProduct(product);
            }
            catch (Exception ex)
            {
                // 记录错误并重新抛出
                throw new Exception("创建商品失败: " + ex.Message, ex);
            }
        }

        /// <summary>
        /// 更新商品
        /// </summary>
        public bool UpdateProduct(Product product)
        {
            try
            {
                // 业务逻辑验证
                if (product.ProductId <= 0)
                {
                    throw new ArgumentException("商品ID无效");
                }

                if (string.IsNullOrWhiteSpace(product.ProductName))
                {
                    throw new ArgumentException("商品名称不能为空");
                }

                if (string.IsNullOrWhiteSpace(product.Sku))
                {
                    throw new ArgumentException("商品SKU不能为空");
                }

                // 检查分类是否存在
                var category = _categoryRepo.GetProductCategoryById(product.CategoryId);
                if (category == null)
                {
                    throw new ArgumentException("无效的商品分类");
                }

                // 验证价格
                if (product.CostPrice < 0)
                {
                    throw new ArgumentException("成本价格不能小于0");
                }

                if (product.SalePrice < 0)
                {
                    throw new ArgumentException("销售价格不能小于0");
                }

                if (product.SalePrice < product.CostPrice)
                {
                    throw new ArgumentException("销售价格不能低于成本价格");
                }

                // 验证安全库存
                if (product.SafeStock < 0)
                {
                    throw new ArgumentException("安全库存不能小于0");
                }

                // 更新最后修改日期
                product.LastUpdated = DateTime.Now;

                // 调用数据访问层更新商品
                return _productRepo.UpdateProduct(product);
            }
            catch (Exception ex)
            {
                // 记录错误并重新抛出
                throw new Exception("更新商品失败: " + ex.Message, ex);
            }
        }

        /// <summary>
        /// 删除商品
        /// </summary>
        public bool DeleteProduct(int productId)
        {
            try
            {
                // 业务逻辑验证
                if (productId <= 0)
                {
                    throw new ArgumentException("商品ID无效");
                }

                // 检查商品是否存在
                var product = _productRepo.GetProductById(productId);
                if (product == null)
                {
                    throw new ArgumentException("商品不存在");
                }

                // 这里可以添加检查商品是否已被订单或库存使用的逻辑

                // 调用数据访问层删除商品
                return _productRepo.DeleteProduct(productId);
            }
            catch (Exception ex)
            {
                // 记录错误并重新抛出
                throw new Exception("删除商品失败: " + ex.Message, ex);
            }
        }
    }
}