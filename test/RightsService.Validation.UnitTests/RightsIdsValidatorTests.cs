﻿using FluentValidation;
using UniversityHelper.RightsService.Data.Interfaces;
using UniversityHelper.RightsService.Models.Db;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using System.Collections.Generic;

namespace UniversityHelper.RightsService.Validation.UnitTests;

public class RightsIdsValidatorTests
{
  private IValidator<IEnumerable<int>> validator;
  private List<DbRightLocalization> existingRightsList;
  private Mock<IRightLocalizationRepository> repositoryMock;
  private Mock<IMemoryCache> cacheMock;

  private int rightId;
  private DbRightLocalization dbRight;
  private object right;

  //[SetUp]
  //public void SetUp()
  //{
  //    repositoryMock = new Mock<IRightLocalizationRepository>();
  //    cacheMock = new Mock<IMemoryCache>();

  //    rightId = 1;
  //    dbRight = new DbRightsLocalization { RightId = rightId };
  //    existingRightsList = new List<DbRightsLocalization>() { dbRight };

  //    repositoryMock
  //        .Setup(r => r.GetRightsListAsync())
  //        .Returns(existingRightsList);

  //    validator = new RightsIdsValidator(repositoryMock.Object, cacheMock.Object);
  //}

  //[Test]
  //public void ShouldThrowValidationExceptionWhenRightListIsEmpty()
  //{
  //    validator.ShouldHaveValidationErrorFor(x => x, new List<int>());
  //}

  //[Test]
  //public void ShouldThrowValidationExceptionWhenRightIdIsInvalid()
  //{
  //    repositoryMock
  //        .Setup(x => x.GetRightsList())
  //        .Returns(existingRightsList);

  //    validator.ShouldHaveValidationErrorFor(x => x, new List<int>() { -1 });
  //}

  //[Test]
  //public void ShouldThrowValidationExceptionWhenRightDoesNotExist()
  //{
  //    cacheMock
  //        .Setup(x => x.TryGetValue(It.IsAny<object>(), out right))
  //        .Returns(false);

  //    repositoryMock
  //        .Setup(x => x.GetRightsList())
  //        .Returns(existingRightsList);

  //    validator.ShouldHaveValidationErrorFor(x => x, new List<int>() { 10000 });
  //}

  //[Test]
  //public void ShouldValidateSuccessfullyWhenRightsListExistInDb()
  //{
  //    cacheMock
  //        .Setup(x => x.TryGetValue(It.IsAny<object>(), out right))
  //        .Returns(false);

  //    repositoryMock
  //        .Setup(x => x.GetRightsList())
  //        .Returns(existingRightsList);

  //    cacheMock
  //        .Setup(x => x.CreateEntry(It.IsAny<object>()))
  //        .Returns(Mock.Of<ICacheEntry>());

  //    validator.ShouldNotHaveValidationErrorFor(x => x, new List<int>() { rightId });
  //}

  //[Test]
  //public void ShouldValidateSuccessfullyWhenRightsListExistInCache()
  //{
  //    cacheMock
  //        .Setup(x => x.TryGetValue(It.IsAny<object>(), out right))
  //        .Returns(true);

  //    validator.ShouldNotHaveValidationErrorFor(x => x, new List<int>() { rightId });
  //}
}
