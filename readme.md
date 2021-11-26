# .Net Randomization

.Net Randomization is an open source library designed to have the capabilities of the most random number generator available. Using System.Random can sometimes cause issues with it's inefficient random nature, by using the System.Security.Cryptography.RandomNumberGenerator we can improve the efficient of generating truly random numbers.

## Analytics

![GitHub](https://img.shields.io/github/license/vaeyori/dotnet-randomization?label=License)
![GitHub last commit](https://img.shields.io/github/last-commit/vaeyori/dotnet-randomization?label=Latest%20Commit)
[![Build Status](https://dev.azure.com/vaeyori/Vaeyori/_apis/build/status/Vaeyori.dotnet-randomization?branchName=main)](https://dev.azure.com/vaeyori/Vaeyori/_build/latest?definitionId=8&branchName=main)
![Azure DevOps tests](https://img.shields.io/azure-devops/tests/vaeyori/Vaeyori/8?label=Test%20Results)
![Azure DevOps coverage](https://img.shields.io/azure-devops/coverage/vaeyori/Vaeyori/8?label=Code%20Coverage)
![GitHub tag (latest SemVer)](https://img.shields.io/github/v/tag/vaeyori/dotnet-weightedmatrix?label=Version&sort=semver)
![Libraries.io dependency status for GitHub repo](https://img.shields.io/librariesio/github/vaeyori/dotnet-randomization?label=Dependencies)

## Usage

### Installation

Install via Package Manager for abstract implementation

    Install-Package Vaeyori.Randomization.Abstractions

Install via Package Manager for concrete implementation

    Install-Package Vaeyori.Randomization

## Contribute

Contributions are always welcome! Please read the [contribution guidelines](/contributing.md) first.

## License

[GNU AGPLv3](https://choosealicense.com/licenses/agpl-3.0/)
