describe('template spec', () => {
  before(() => {
    cleanupPolls();

    cy.request("POST", "http://localhost:5295/poll", {
        name: "Test Poll 1",
        options: [
          {
            name: "Option 1",
          },
          {
            name: "Option 2",
          },
        ],
      });

    cy.request("POST", "http://localhost:5295/poll", {
      name: "Test Poll 2",
      options: [
        {
          name: "Option 1",
        },
        {
          name: "Option 2",
        },
      ],
    });
  });

  it("The Home Page should show a list of Polls", () => {
    cy.visit("http://localhost:3000")
    cy.get("[data-test='poll-list']").children().should('have.length', 2)
  });

  it("The Home page should have a button for adding a poll that should allow for adding a new poll", () => {
    cy.visit("http://localhost:3000");
    cy.get("[data-test='poll-add-button']").click();
    cy.get("[poll-add-test='poll-name-input']").type("Poll");
    cy.get("[poll-add-test='option-add").click();
    cy.get("[poll-add-test='option-add").click();
    cy.get("[poll-add-test='option-add").click();

    cy.get("[poll-add-test='option-name-input']")
      .first()
      .type("Option 1")
      .should("have.value", "Option 1")
      .next("input")
      .type("Option 2")
      .should("have.value", "Option 2")
      .next("input")
      .type("Option 3")
      .should("have.value", "Option 3");
    
      cy.get("[poll-add-test='submit-poll']").click()
      cy.get("[data-test='poll-list']").children().should("have.length", 3);

      cy.reload()
      cy.get("[data-test='poll-list']").children().should("have.length", 3);
    })

  after(() => {
    cleanupPolls()
  })
})

interface Polls {
  id: number
}
function cleanupPolls() {
    cy.request("http://localhost:5295/poll").then((response) => {
      let polls = response.body as Array<Polls>;
      polls.map((poll) => {
        let url = "http://localhost:5295/poll/" + poll.id;
        cy.request("DELETE", url);
      });
    });
}