describe('template spec', () => {
  it("the h1 contains the correct text", () => {
    cy.visit("http://localhost:3000");
    cy.get("[data-test='test-heading']").contains("TESaT")
  });
})