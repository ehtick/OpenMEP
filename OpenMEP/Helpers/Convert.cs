﻿using System.Reflection;
using Autodesk.DesignScript.Runtime;
using Autodesk.Revit.DB;
using Revit.Elements;
using Revit.GeometryConversion;
using RevitServices.Persistence;
using dynCategory = Revit.Elements.Category;
using dynDocument = Revit.Application.Document;
using dynElement = Revit.Elements.Element;
using dynElementSelector = Revit.Elements.ElementSelector;
using dynFamilyParameter = Revit.Elements.FamilyParameter;
using dynParameter = Revit.Elements.Parameter;
using rvtCategory = Autodesk.Revit.DB.Category;
using rvtDocument = Autodesk.Revit.DB.Document;
using rvtElement = Autodesk.Revit.DB.Element;
using rvtFamilyParameter = Autodesk.Revit.DB.FamilyParameter;
using rvtParameter = Autodesk.Revit.DB.Parameter;
using Surface = Autodesk.DesignScript.Geometry.Surface;

namespace OpenMEP.Helpers
{
    [IsVisibleInDynamoLibrary(false)]
    internal static class Convert
    {

        /// <summary>
        /// convert Revit document to Dynamo document
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        internal static dynDocument? ToDynamoType(this rvtDocument item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            var constructor = typeof(dynDocument).GetConstructors(
                BindingFlags.NonPublic | BindingFlags.Instance).FirstOrDefault();
            return constructor!.Invoke(new object[] {item}) as dynDocument;
        }

        /// <summary>
        /// Convert Dynamo document to Revit document
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        internal static rvtDocument? ToRevitType(this dynDocument item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            var property = typeof(dynDocument).GetProperty("InternalDocument",
                BindingFlags.NonPublic | BindingFlags.Instance);

            return property!.GetValue(item) as rvtDocument;
        }


        /// <summary>
        /// Convert Revit Category to Dynamo Category
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        internal static dynCategory? ToDynamoType(this rvtCategory item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            return dynCategory.ById(item.Id.IntegerValue);
        }

        /// <summary>
        /// Convert Dynamo Category to Revit Category
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        internal static rvtCategory ToRevitType(this dynCategory item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            var document = DocumentManager.Instance.CurrentDBDocument;
            var id = new ElementId(item.Id);
            return rvtCategory.GetCategory(document, id);
        }

        /// <summary>
        /// Convert Revit Category to Dynamo Category
        /// </summary>
        /// <param name="item"></param>
        /// <param name="document"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        internal static rvtCategory ToRevitType(
            this dynCategory item, rvtDocument document)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            var id = new ElementId(item.Id);
            return rvtCategory.GetCategory(document, id);
        }

        /// <summary>
        /// Convert Revit Parameter to Dynamo Parameter
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        internal static dynParameter? ToDynamoType(this rvtParameter item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            var constructor = typeof(dynParameter).GetConstructors(
                BindingFlags.NonPublic | BindingFlags.Instance).FirstOrDefault();

            return constructor!.Invoke(new object[] {item}) as dynParameter;
        }

        /// <summary>
        /// Convert Revit FamilyParameter to Dynamo FamilyParameter
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        internal static dynFamilyParameter? ToDynamoType(this rvtFamilyParameter item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            var constructor = typeof(dynFamilyParameter).GetConstructors(
                BindingFlags.NonPublic | BindingFlags.Instance).FirstOrDefault();
            return constructor!.Invoke(new object[] {item}) as dynFamilyParameter;
        }

        /// <summary>
        /// Convert Dynamo Parameter to Revit Parameter
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        internal static rvtParameter? ToRevitType(this dynParameter item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            var property = typeof(dynParameter)
                .GetProperty("InternalParameter",
                    BindingFlags.NonPublic | BindingFlags.Instance);
            return property!.GetValue(item) as rvtParameter;
        }

        /// <summary>
        /// Convert Dynamo FamilyParameter to Revit FamilyParameter
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        internal static rvtFamilyParameter? ToRevitType(this dynFamilyParameter item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            var property = typeof(dynParameter)
                .GetProperty("InternalFamilyParameter",
                    BindingFlags.NonPublic | BindingFlags.Instance);
            return property!.GetValue(item) as rvtFamilyParameter;
        }


        /// <summary>
        /// Convert Revit ElementId To Dynamo Element
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        internal static dynElement ToDynamoType(this ElementId item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            return dynElementSelector.ByElementId(item.IntegerValue);
        }

        /// <summary>
        /// Convert Element Interger Id to Dynamo Element
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        internal static dynElement ToDynamoType(this int item)
        {
            return dynElementSelector.ByElementId(item);
        }

        /// <summary>
        /// Convert Revit Element to Dynamo Element
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        internal static dynElement? ToDynamoType(this rvtElement? item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            return item.ToDSType(true);
        }

        /// <summary>
        /// Convert IEnumerable of Revit Element to Dynamo Element
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        internal static IEnumerable<dynElement> ToDynamoType(this IEnumerable<rvtElement> items)
        {
            if (!items.Any()) throw new ArgumentNullException(nameof(items));
            foreach (var item in items)
            {
                yield return item.ToDSType(true);
            }
        }

        /// <summary>
        /// Convert Dynamo Element to Revit Element
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        internal static rvtElement ToRevitType(this dynElement item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            return item.InternalElement;
        }

        /// <summary>
        /// Convert Revit ElementId to Revit Element
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        internal static rvtElement ToRevitType(this ElementId item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            var document = DocumentManager.Instance.CurrentDBDocument;
            return document.GetElement(item);
        }

        /// <summary>
        /// Convert Revit ElementId to Revit Element
        /// </summary>
        /// <param name="item"></param>
        /// <param name="document"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        internal static rvtElement ToRevitType(this ElementId item, rvtDocument document)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            return document.GetElement(item);
        }

        /// <summary>
        /// Convert Revit XYZ to Dynamo Point
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        internal static Autodesk.DesignScript.Geometry.Point ToDynamoType(this XYZ item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            return Autodesk.DesignScript.Geometry.Point.ByCoordinates(item.X, item.Y, item.Z);
        }

        /// <summary>
        /// Convert Dynamo Point to Revit XYZ
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        internal static Autodesk.Revit.DB.XYZ ToRevitType(this Autodesk.DesignScript.Geometry.Point item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            return new Autodesk.Revit.DB.XYZ(item.X, item.Y, item.Z);
        }

        /// <summary>
        /// Convert Dynamo Vector to Revit XYZ
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        internal static Autodesk.Revit.DB.XYZ ToRevitType(this Autodesk.DesignScript.Geometry.Vector item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            return new Autodesk.Revit.DB.XYZ(item.X, item.Y, item.Z);
        }

        /// <summary>
        /// Convert Revit XYZ to Dynamo Vector
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        internal static Autodesk.DesignScript.Geometry.Vector ToDynamoVector(this XYZ item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            return Autodesk.DesignScript.Geometry.Vector.ByCoordinates(item.X, item.Y, item.Z);
        }

        /// <summary>
        /// Convert Revit Curve to Dynamo Curve
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        internal static Autodesk.DesignScript.Geometry.Curve ToDynamoType(this Curve item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            return item.ToProtoType();
            ;
        }

        /// <summary>
        /// Convert Dynamo Curve to Revit Line
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        internal static Autodesk.Revit.DB.Line ToRevitLineType(this Autodesk.DesignScript.Geometry.Curve item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            var start = item.StartPoint.ToRevitType();
            var end = item.EndPoint.ToRevitType();
            return Autodesk.Revit.DB.Line.CreateBound(start, end);
        }

        /// <summary>
        ///  Convert Dynamo Curve to Revit Arc
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        internal static Autodesk.Revit.DB.Arc ToRevitArcType(this Autodesk.DesignScript.Geometry.Curve item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            var start = item.StartPoint.ToRevitType();
            var end = item.EndPoint.ToRevitType();
            var center = item.PointAtParameter(0.5).ToRevitType();
            return Autodesk.Revit.DB.Arc.Create(start, end, center);
        }

        /// <summary>
        /// Convert Revit Line to Dynamo Curve
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        internal static Autodesk.DesignScript.Geometry.Curve ToDynamoType(this Line item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            var start = item.GetEndPoint(0).ToDynamoType();
            var end = item.GetEndPoint(1).ToDynamoType();
            return Autodesk.DesignScript.Geometry.Line.ByStartPointEndPoint(start, end);
        }

        /// <summary>
        /// Convert Revit Arc to Dynamo Arc
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        internal static Autodesk.DesignScript.Geometry.Arc ToDynamoType(this Arc item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            var center = item.Center.ToDynamoType();
            var start = item.GetEndPoint(0).ToDynamoType();
            var end = item.GetEndPoint(1).ToDynamoType();
            return Autodesk.DesignScript.Geometry.Arc.ByCenterPointStartPointEndPoint(center, start, end);
        }

        /// <summary>
        /// Convert Dynamo Curve to Revit Curve
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Exception"></exception>
        internal static Autodesk.Revit.DB.Curve ToRevitType(this Autodesk.DesignScript.Geometry.Curve item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            if (item is Autodesk.DesignScript.Geometry.Line)
            {
                return item.ToRevitLineType();
            }

            if (item is Autodesk.DesignScript.Geometry.Arc)
            {
                return item.ToRevitArcType();
            }

            //TODO : With Curve Not is line or Arc
            throw new Exception("Curve type not supported");
        }

        /// <summary>
        /// Convert Revit Plane to Dynamo Plane
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        internal static Autodesk.DesignScript.Geometry.Plane ToDynamoType(this Plane item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            var origin = item.Origin.ToDynamoType();
            var normal = item.Normal.ToDynamoVector();
            return Autodesk.DesignScript.Geometry.Plane.ByOriginNormal(origin, normal);
        }

        /// <summary>
        /// Convert Dynamo Plane to Revit Plane
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        internal static Autodesk.Revit.DB.Plane ToRevitType(this Autodesk.DesignScript.Geometry.Plane item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            var origin = item.Origin.ToRevitType();
            var normal = item.Normal.ToRevitType();
            return Autodesk.Revit.DB.Plane.CreateByNormalAndOrigin(normal, origin);
        }

        internal static IEnumerable<Surface> ToDynamoType(this Face item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            IEnumerable<Surface> surfaces = item.ToProtoType();
            return surfaces;
        }
    }
}