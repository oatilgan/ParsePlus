﻿using System.Collections.Generic;
using System.Linq;
using System.Xml;
using ParsePlus.Bll.Dto;

namespace ParsePlus.Bll.Manager
{
    public class XmlManager
    {
        public List<XmlNodes> GetXmlNodes(string filePath)
        {
            var list = new List<XmlNodes>();

            var doc = new XmlDocument();
            doc.Load(filePath);

            foreach (XmlNode node in doc.DocumentElement)
            {
                var nodeItem = new XmlNodes
                {
                    NodeName = node.Name,
                    SubNodeList = new List<XmlNodes>()
                };

                foreach (XmlNode item in node.ChildNodes)
                {
                    var nodee = GetNodeName(item);
                    if (!list.Any(z => z.NodeName == nodee.NodeName) &&
                        !nodeItem.SubNodeList.Any(z => z.NodeName == nodee.NodeName))
                    {
                        nodeItem.SubNodeList.Add(nodee);
                    }
                }

                if (!list.Any(z => z.NodeName == node.Name) &&
                    !list.Any(z => z.SubNodeList.Any(a => a.NodeName == node.Name)))
                    list.Add(nodeItem);
            }
            return list;
        }

        private XmlNodes GetNodeName(XmlNode node)
        {
            switch (node.NodeType)
            {
                case XmlNodeType.Element:
                    return new XmlNodes { NodeName = node.Name };
            }
            return null;
        }
    }

}
