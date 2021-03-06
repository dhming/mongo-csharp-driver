﻿/* Copyright 2010-2011 10gen Inc.
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
* http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;

using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace MongoDB.DriverOnlineTests.Jira.CSharp93 {
    [TestFixture]
    public class CSharp93Tests {
        [Test]
        public void TestDropAllIndexes() {
            var server = MongoServer.Create("mongodb://localhost/?safe=true");
            var database = server["onlinetests"];
            var collection = database.GetCollection("csharp93");

            if (collection.Exists()) {
                collection.DropAllIndexes();
            } else {
                collection.Insert(new BsonDocument()); // make sure collection exists
            }

            collection.EnsureIndex("x", "y");
            collection.DropIndex("x", "y");

            collection.EnsureIndex(IndexKeys.Ascending("x", "y"));
            collection.DropIndex(IndexKeys.Ascending("x", "y"));
        }

        [Test]
        public void EnsureIndex_SetUniqueTrue_Success() {
            var server = MongoServer.Create("mongodb://localhost/?safe=true");
            var database = server["onlinetests"];
            var collection = database.GetCollection("csharp93");

            if (collection.Exists()) {
                collection.DropAllIndexes();
            } else {
                collection.Insert(new BsonDocument()); // make sure collection exists
            }

            collection.EnsureIndex(IndexKeys.Ascending("x"), IndexOptions.SetUnique(true));
            collection.EnsureIndex(IndexKeys.Ascending("y"), IndexOptions.SetUnique(false));
        }
    }
}
