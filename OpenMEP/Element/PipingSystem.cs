﻿using Autodesk.Revit.DB.Plumbing;
using OpenMEP.Helpers;
using RevitServices.Persistence;

namespace OpenMEP.Element;

public class PipingSystem
{
    private PipingSystem()
    {
    }

    /// <summary>
    /// flag true to return all piping systems
    /// </summary>
    /// <param name="toggle">flag true or false to fresh</param>
    /// <returns name="pipeSystemTypes">pipeSystemTypes</returns>
    public static IEnumerable<Revit.Elements.Element?> GetAllPipeSystemTypes(bool toggle)
    {
        // filter for all piping systems
        Autodesk.Revit.DB.FilteredElementCollector collector = new Autodesk.Revit.DB.FilteredElementCollector(DocumentManager.Instance.CurrentDBDocument);
        Autodesk.Revit.DB.ElementClassFilter filter = new Autodesk.Revit.DB.ElementClassFilter(typeof(Autodesk.Revit.DB.Plumbing.PipingSystemType));
        Autodesk.Revit.DB.FilteredElementIterator iterator = collector.WherePasses(filter).GetElementIterator();
        iterator.Reset();
        while (iterator.MoveNext())
        {
            Autodesk.Revit.DB.Element element = iterator.Current!;
            if (element is PipingSystemType pipingSystemType)
            {
                
                yield return pipingSystemType.ToDynamoType();
            }
        }
    }
    
    /// <summary>
    /// return pipe system type by name
    /// </summary>
    /// <param name="typeName">name of pipe system type</param>
    /// <returns name="pipeSystemType">the element system type</returns>
    public static Revit.Elements.Element? GetPipeSystemTypeByName(string typeName)
    {
        return GetAllPipeSystemTypes(true).FirstOrDefault(x => x!.Name == typeName);
    }

}