using System;
using System.Collections.Generic;
using System.Linq;
using CSproject.Business.Models;
using CSproject.Data.Repositories;

namespace CSproject.Business.Services
{
    public class ProductCategoryService
    {
        private readonly ProductCategoryRepository _categoryRepo = new ProductCategoryRepository();

        /// <summary>
        /// 获取商品分类列表
        /// </summary>
        public List<ProductCategory> GetProductCategories(string categoryName = null, string status = null)
        {
            return _categoryRepo.GetProductCategories(categoryName, status);
        }

        /// <summary>
        /// 获取所有商品分类列表（用于下拉框）
        /// </summary>
        public List<ProductCategory> GetAllProductCategories()
        {
            return _categoryRepo.GetAllProductCategories();
        }

        /// <summary>
        /// 获取商品分类详情
        /// </summary>
        public ProductCategory GetProductCategoryById(int categoryId)
        {
            return _categoryRepo.GetProductCategoryById(categoryId);
        }

        /// <summary>
        /// 获取活跃商品分类列表
        /// </summary>
        public List<ProductCategory> GetActiveProductCategories()
        {
            return _categoryRepo.GetActiveProductCategories();
        }

        /// <summary>
        /// 创建商品分类
        /// </summary>
        public int CreateProductCategory(ProductCategory category)
        {
            try
            {
                // 业务逻辑验证
                if (string.IsNullOrWhiteSpace(category.CategoryName))
                {
                    throw new ArgumentException("分类名称不能为空");
                }

                // 设置默认状态
                if (string.IsNullOrWhiteSpace(category.Status))
                {
                    category.Status = "1"; // 活跃
                }

                // 设置日期
                category.CreatedDate = DateTime.Now;
                category.LastUpdated = DateTime.Now;

                // 调用数据访问层创建分类
                return _categoryRepo.CreateProductCategory(category);
            }
            catch (Exception ex)
            {
                // 记录错误并重新抛出
                throw new Exception("创建商品分类失败: " + ex.Message, ex);
            }
        }

        /// <summary>
        /// 更新商品分类
        /// </summary>
        public bool UpdateProductCategory(ProductCategory category)
        {
            try
            {
                // 业务逻辑验证
                if (string.IsNullOrWhiteSpace(category.CategoryName))
                {
                    throw new ArgumentException("分类名称不能为空");
                }

                if (category.CategoryId <= 0)
                {
                    throw new ArgumentException("分类ID无效");
                }

                // 更新最后修改日期
                category.LastUpdated = DateTime.Now;

                // 调用数据访问层更新分类
                return _categoryRepo.UpdateProductCategory(category);
            }
            catch (Exception ex)
            {
                // 记录错误并重新抛出
                throw new Exception("更新商品分类失败: " + ex.Message, ex);
            }
        }

        /// <summary>
        /// 删除商品分类
        /// </summary>
        public bool DeleteProductCategory(int categoryId)
        {
            try
            {
                // 业务逻辑验证
                if (categoryId <= 0)
                {
                    throw new ArgumentException("分类ID无效");
                }

                // 检查是否有子分类或关联商品
                var categories = GetProductCategories();
                if (categories.Any(c => c.ParentCategoryId == categoryId))
                {
                    throw new InvalidOperationException("该分类下存在子分类，无法删除");
                }

                // 调用数据访问层删除分类
                return _categoryRepo.DeleteProductCategory(categoryId);
            }
            catch (Exception ex)
            {
                // 记录错误并重新抛出
                throw new Exception("删除商品分类失败: " + ex.Message, ex);
            }
        }
    }
}