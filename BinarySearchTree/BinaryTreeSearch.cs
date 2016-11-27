using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinarySearchTree
{
    public sealed class BinaryTreeSearch<T> : IEnumerable<T>
    {
        #region Fields and constructor

        private Node _root;
        private readonly IComparer<T> _comparer;

        /// <summary>
        /// The constructor takes as parameters the collection of items and a comparator.
        /// The value of the comparator can be passed null. 
        /// Then it will be used by default comparator.
        /// </summary>
        /// <param name="collection">collection of items.</param>
        /// <param name="comparer">The comparator should be sent for the user types.</param>
        public BinaryTreeSearch(IEnumerable<T> collection, IComparer<T> comparer)
        {
            if (ReferenceEquals(null, collection))
                throw new ArgumentNullException(nameof(collection));
            if (ReferenceEquals(null, comparer))
            {
                _comparer = Comparer<T>.Default;

            }
            else
            {
                _comparer = comparer;
            }
            foreach (var elem in collection)
            {
                Add(elem);
            }
        }

        /// <summary>
        /// The constructor takes as parameters the collection of items.
        /// The value of the comparator can be passed null. 
        /// Then it will be used by default comparator.
        /// </summary>
        /// <param name="collection">collection of items.</param>
        public BinaryTreeSearch(IEnumerable<T> collection) : this(collection, null)
        {
        }

        public BinaryTreeSearch() { }
        #endregion

        #region implement interfaces

        /// <summary>Returns an enumerator that iterates through the collection.</summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            return InOrder().GetEnumerator();
        }

        /// <summary>Returns an enumerator that iterates through a collection.</summary>
        /// <returns>An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion

        #region Methods
        /// <summary>Adds an items to the <see cref="BinaryTree{T}"/></summary>
        /// <param name="collection">The collection to add to the <see cref="BinaryTree{T}"/></param>
        public void AddRange(IEnumerable<T> collection)
        {
            foreach (var value in collection)
            {
                Add(value);
            }
        }

        /// <summary>
        /// Method added element in binary tree.
        /// </summary>
        /// <param name="elem">it's the leave which will be add.</param>
        public void Add(T elem)
        {

            if (ReferenceEquals(null, elem))
                throw new ArgumentNullException();
            if (ReferenceEquals(null, _root))
            {
                _root = new Node(elem);
                return;
            }
            Node currentRoot = _root;
            Node dadyRoot = null;

            while (!ReferenceEquals(null, currentRoot))
            {
                if (_comparer.Compare(currentRoot.Value, elem) == 0)
                    return;
                dadyRoot = currentRoot;
                if (_comparer.Compare(currentRoot.Value, elem) < 0)
                    currentRoot = currentRoot.Right;
                else
                    currentRoot = currentRoot.Left;
            }

            if (_comparer.Compare(dadyRoot.Value, elem) > 0)
                dadyRoot.Left = new Node(elem);
            else
                dadyRoot.Right = new Node(elem);

        }

        /// <summary>
        /// Method find element in binary tree.
        /// </summary>
        /// <param name="elem">it's the leave which will be find.</param>
        public bool Contains(T elem)
        {

            if (ReferenceEquals(null, elem))
                throw new ArgumentNullException();
            if (ReferenceEquals(null, _root))
            {
                _root = new Node(elem);
                return false;
            }
            Node currentRoot = _root;
            Node dadyRoot = null;

            while (!ReferenceEquals(null, currentRoot))
            {
                if (_comparer.Compare(currentRoot.Value, elem) == 0)
                    return true;
                dadyRoot = currentRoot;
                if (_comparer.Compare(currentRoot.Value, elem) < 0)
                    currentRoot = currentRoot.Right;
                else
                    currentRoot = currentRoot.Left;
            }
            return false;

        }

        /// <summary>
        /// Method clear binary tree.
        /// </summary>
        public void Clear()
        {
            _root = null;

        }

        /// <summary>
        /// Performs direct traversal of the tree.
        /// </summary>
        /// <returns>elements tree of IEnumerable type.</returns>
        public IEnumerable<T> PreOrder() => PreOrder(_root);

        /// <summary>
        /// Performs reverse traversal of the tree.
        /// </summary>
        /// <returns>elements tree of IEnumerable type.</returns>
        public IEnumerable<T> PostOrder() => PostOrder(_root);

        /// <summary>
        /// Performs a symmetrical tree traversal.
        /// </summary>
        /// <returns>elements tree of IEnumerable type.</returns>
        public IEnumerable<T> InOrder() => InOrder(_root);
        #endregion

        #region private components
        private class Node
        {
            private T _value;
            public T Value
            {
                get { return _value; }
            }
            public Node Left { get; set; }
            public Node Right { get; set; }
            public Node(T value)
            {
                _value = value;
            }

        }

        private IEnumerable<T> PostOrder(Node node)
        {
            if (node == null)
                yield break;

            foreach (var e in PostOrder(node.Left))
                yield return e;

            foreach (var e in PostOrder(node.Right))
                yield return e;

            yield return node.Value;
        }


        private IEnumerable<T> PreOrder(Node node)
        {
            if (node == null)
                yield break;

            yield return node.Value;

            foreach (var e in PreOrder(node.Left))
                yield return e;

            foreach (var e in PreOrder(node.Right))
                yield return e;
        }

        private IEnumerable<T> InOrder(Node node)
        {
            if (node == null) yield break;
            foreach (var n in InOrder(node.Left))
                yield return n;

            yield return node.Value;
            foreach (var n in InOrder(node.Right))
                yield return n;
        }
        #endregion
    }
}
