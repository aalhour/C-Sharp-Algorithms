using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DataStructures.Common;

namespace DataStructures.Trees
{
    public static class TreeDrawer
    {
        /// <summary>
        /// Public API.
        /// Extension method for BinarySearchTree<T>.
        /// Returns a visualized binary search tree text.
        /// </summary>
        public static string DrawTree<T>(this IBinarySearchTree<T> tree) where T : IComparable<T>
        {
            int position, width;
            return String.Join("\n", _recursivelyDrawTree(tree.Root, out position, out width));
        }


        /// <summary>
        /// /// Recusively draws the tree starting from node.
        /// To construct a full tree representation concatenate the returned list of strings by '\n'.
        /// 
        /// Example:
        /// int position, width;
        /// var fullTree = String.Join("\n", _recursivelyDrawTree(this.Root, out position, out width));
        /// 
        /// Algorithm developed by MIT OCW.
        /// http://ocw.mit.edu/courses/electrical-engineering-and-computer-science/6-006-introduction-to-algorithms-fall-2011/readings/binary-search-trees/bstsize_r.py
        /// </summary>
        /// <param name="node"></param>
        /// <param name="positionOutput"></param>
        /// <param name="widthOutput"></param>
        /// <returns>List of tree levels as strings.</returns>
        private static List<string> _recursivelyDrawTree<T>(BSTNode<T> node, out int positionOutput, out int widthOutput) where T : IComparable<T>
        {
            widthOutput = 0;
            positionOutput = 0;
            List<string> listOfLines = new List<string>();

            if (node == null)
            {
                return listOfLines;
            }

            //
            // Variables
            string nodeLabel;

            List<string> leftLines, rightLines;
            leftLines = rightLines = new List<string>();

            int leftPosition = 0, rightPosition = 0;
            int leftWidth = 0, rightWidth = 0;
            int middle, position_out, width_out;

            //
            // Start drawing
            nodeLabel = Convert.ToString(node.Value);

            // Visit the left child
            leftLines = _recursivelyDrawTree(node.LeftChild, out leftPosition, out leftWidth);

            // Visit the right child
            rightLines = _recursivelyDrawTree(node.RightChild, out rightPosition, out rightWidth);

            // Calculate pads
            middle = Math.Max(Math.Max(2, nodeLabel.Length), (rightPosition + leftWidth - leftPosition + 1));
            position_out = leftPosition + middle;
            width_out = leftPosition + middle + rightWidth - rightPosition;

            while (leftLines.Count < rightLines.Count)
                leftLines.Add(new String(' ', leftWidth));

            while (rightLines.Count < leftLines.Count)
                rightLines.Add(new String(' ', rightWidth));

            if ((middle - nodeLabel.Length % 2 == 1) && (nodeLabel.Length < middle) && (node.Parent != null && node.IsLeftChild))
                nodeLabel += ".";

            // Format the node's label
            nodeLabel = nodeLabel.PadCenter(middle, '.');

            var nodeLabelChars = nodeLabel.ToCharArray();

            if (nodeLabelChars[0] == '.')
                nodeLabelChars[0] = ' ';

            if (nodeLabelChars[nodeLabelChars.Length - 1] == '.')
                nodeLabelChars[nodeLabelChars.Length - 1] = ' ';

            nodeLabel = String.Join("", nodeLabelChars);

            //
            // Construct the list of lines.
            listOfLines = new List<string>()
            {
                // 0
                (new String(' ', leftPosition )) + nodeLabel + (new String(' ', (rightWidth - rightPosition))),

                // 1
                (new String(' ', leftPosition)) + "/" + (new String(' ', (middle - 2))) + "\\" + (new String(' ', (rightWidth - rightPosition))) 
            };

            //
            // Add the right lines and left lines to the final list of lines.
            listOfLines =
                listOfLines.Concat(
                    leftLines.Zip(
                        rightLines, (left_line, right_line) =>
                            left_line + (new String(' ', (width_out - leftWidth - rightWidth))) + right_line)
                ).ToList();

            //
            // Return
            widthOutput = width_out;
            positionOutput = position_out;
            return listOfLines;
        }
    }
}
