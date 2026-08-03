# Refactoring Roadmap

Each checkbox should be a small task or commit. Keep behavior covered by tests throughout the exercise.

## 0. Establish a safety net

- [ ] Add a test project and reference the production project.
- [ ] Capture the sample receipt as a golden-master fixture.
- [ ] Test an empty basket, comments, malformed rows, every discount, every tax category, discount capping, and name truncation.

## 1. Deciding names and locations for types

- [ ] Create `Domain/Products`, `Domain/Pricing`, `Domain/Receipts`, `Application`, `Infrastructure/Files`, and `Infrastructure/CommandLine`.
- [ ] Introduce `Product`, `BasketItem`, `ReceiptLine`, and `Receipt`.
- [ ] Replace ambiguous local names.
- [ ] Reduce `Program.cs` to composition and invocation.

## 2. Decoupling implementation with Strategies

- [ ] Define `IDiscountStrategy` and implement quantity, book, and clearance strategies.
- [ ] Define `ITaxStrategy` and implement tax-free, standard, and luxury strategies.
- [ ] Define `IProductNameFormatter` and implement truncation.
- [ ] Replace embedded calculations with these strategies one at a time.

## 3. Chaining implementation with Composite and Decorator

- [ ] Add `CompositeDiscountStrategy` to combine applicable discounts.
- [ ] Add `CappedDiscountStrategy` as a decorator.
- [ ] Add `RoundingTaxStrategy` as a decorator with an explicit rounding policy.
- [ ] Test composition order and boundary behavior.

## 4. Constructing complex object graphs with Builder

- [ ] Add `ReceiptGeneratorBuilder`.
- [ ] Add methods for discount, tax resolution, formatting, reading, rendering, and writing collaborators.
- [ ] Validate missing mandatory collaborators in `Build`.
- [ ] Add `CreateDefault` and move construction to one composition root.

## 5. Modeling low-level concerns as Infrastructure

- [ ] Extract `IBasketReader` and implement `CsvBasketReader`.
- [ ] Extract `IReceiptWriter` and implement `FileReceiptWriter`.
- [ ] Extract `IReceiptRenderer` and implement `TextReceiptRenderer`.
- [ ] Add `CommandLineOptionsParser` with structured errors and meaningful exit codes.
- [ ] Replace broad exception handling with expected application failures.

## 6. Factoring domain complexity out with the Rules pattern

- [ ] Define `IDiscountRule` and an explicit rule result.
- [ ] Convert quantity, book, and clearance conditions into named rules.
- [ ] Define `ITaxRule` and convert food, luxury, and default taxation into named rules.
- [ ] Add explicit priorities and a resolver that detects missing or ambiguous tax rules.

## 7. Supporting multiple transforms with the Visitor pattern

- [ ] Define `IReceiptElement.Accept` and `IReceiptVisitor`.
- [ ] Make `Receipt` and `ReceiptLine` visitable.
- [ ] Implement text rendering as a visitor.
- [ ] Add a statistics visitor that counts products, units, discounts, and taxes.
- [ ] Optionally add an HTML visitor without changing receipt element types.

## 8. Final quality pass

- [ ] Replace category strings with a category value type or enum.
- [ ] Define the money and rounding policy.
- [ ] Validate names, quantities, and prices.
- [ ] Enable warnings as errors.
- [ ] Run all tests and compare the final sample receipt with the golden master.

Recommended order: tests, names and types, infrastructure, strategies, rules, composite/decorators, builder, visitor, final validation.
