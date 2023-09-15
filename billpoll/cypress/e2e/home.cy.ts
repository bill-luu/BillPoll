describe('template spec', () => {
  before(() => {
    console.log("BEFORE START")
    cy
      .request("POST", "http://localhost:5295/poll", {
        id: "t1",
        name: "Test Poll 1",
        options: [
          {
            id: "1",
            name: "Option 1",
            votes: 0,
          },
          {
            id: "2",
            name: "Option 2",
            votes: 0,
          },
        ],
      });

    cy.request("POST", "http://localhost:5295/poll", {
      id: "t2",
      name: "Test Poll 2",
      options: [
        {
          id: "3",
          name: "Option 1",
          votes: 0,
        },
        {
          id: "4",
          name: "Option 2",
          votes: 0,
        },
      ],
    });
  });

  it("The Home Page should show a list of Polls", () => {
    cy.visit("http://localhost:3000")
    cy.get("[data-test='poll-list']").children().should('have.length', 2)
  });

  after(() => {
    cy.request("DELETE", "http://localhost:5295/poll/t1");
    cy.request("DELETE", "http://localhost:5295/poll/t2");
  })
})