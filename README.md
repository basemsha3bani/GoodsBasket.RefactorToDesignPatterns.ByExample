# Receipt Generator — Refactoring Practice

This repository contains deliberately messy but functional C# code for practicing refactoring and design patterns. The program reads a CSV basket, applies discounts and taxes, and writes a text receipt.

The starting implementation intentionally mixes command-line handling, file I/O, parsing, business rules, calculation, formatting, and error handling. Begin with characterization tests and refactor it in small behavior-preserving commits.

## Run

```powershell
dotnet run --project ReceiptGenerator -- ReceiptGenerator/sample-basket.csv receipt.txt
```

## Current behavior

- Three or more units receive a 10% discount.
- Books receive an additional 5% discount.
- Product names containing `clearance` receive a fixed 2.00 discount.
- Discounts cannot exceed the original line total.
- Food is tax-free, luxury items are taxed at 20%, and other items at 10%.
- Product names longer than 18 characters are truncated on the receipt.

See [REFACTORING_PLAN.md](REFACTORING_PLAN.md) for the exercise roadmap.
