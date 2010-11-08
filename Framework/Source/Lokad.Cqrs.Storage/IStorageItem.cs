﻿#region (c) 2010 Lokad Open Source - New BSD License 

// Copyright (c) Lokad 2010, http://www.lokad.com
// This code is released as Open Source under the terms of the New BSD Licence

#endregion

using System;
using System.IO;

namespace Lokad.Cqrs
{
	/// <summary>
	/// Base interface for performing storage operations against local or remote persistence.
	/// </summary>
	public interface IStorageItem
	{
		/// <summary>
		/// Gets the full path of the current iteб.
		/// </summary>
		/// <value>The full path.</value>
		string FullPath { get; }

		/// <summary>
		/// Performs the write operation, ensuring that the condition is met.
		/// </summary>
		/// <param name="writer">The writer.</param>
		/// <param name="condition">The condition.</param>
		/// <exception cref="StorageItemIntegrityException">when integrity check fails during the upload</exception>
		void Write(Action<Stream> writer, StorageCondition condition = default(StorageCondition));

		/// <summary>
		/// Attempts to read the storage item.
		/// </summary>
		/// <param name="reader">The reader.</param>
		/// <param name="condition">The condition.</param>
		/// <returns>false if the item exists, but the condition was not satisfied</returns>
		/// <exception cref="StorageItemNotFoundException">if the item does not exist.</exception>
		/// <exception cref="StorageContainerNotFoundException">if the container for the item does not exist</exception>
		/// <exception cref="StorageItemIntegrityException">when integrity check fails</exception>
		void ReadInto(ReaderDelegate reader, StorageCondition condition = default(StorageCondition));

		/// <summary>
		/// Removes the item, ensuring that the specified condition is met.
		/// </summary>
		/// <param name="condition">The condition.</param>
		void Remove(StorageCondition condition = default(StorageCondition));

		/// <summary>
		/// Gets the info about this item. It returns empty result if the item does not exist or does not match the condition
		/// </summary>
		/// <param name="condition">The condition.</param>
		/// <returns></returns>
		Maybe<StorageItemInfo> GetInfo(StorageCondition condition = default(StorageCondition));

		/// <summary>
		/// Copies to.
		/// </summary>
		/// <param name="sourceItem">The target.</param>
		/// <param name="condition">The condition.</param>
		/// <param name="copySourceCondition">The copy source condition.</param>
		/// <returns></returns>
		/// <exception cref="StorageItemNotFoundException">when source storage is not found</exception>
		/// <exception cref="StorageItemIntegrityException">when integrity check fails</exception>
		void CopyFrom(IStorageItem sourceItem,
			StorageCondition condition = default(StorageCondition),
			StorageCondition copySourceCondition = default(StorageCondition));
	}
}