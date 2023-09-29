import React from 'react'
import AddPollComponent from './add-poll'

describe('<AddPollComponent />', () => {
  it('renders', () => {
    // see: https://on.cypress.io/mounting-react
    cy.mount(<AddPollComponent updatePolls={() =>{}}/>)
  })

  it('shows the create poll component when the Add Poll button is clicked', () => {
    cy.mount(<AddPollComponent updatePolls={() =>{}}/>)
    cy.get("[data-test='poll-add-button']").click()
    cy.get("[data-test='create-new-poll']").should("exist");
  })

  it('lists an option when "Add Option" is clicked', () => {
    cy.mount(<AddPollComponent updatePolls={() =>{}}/>)
    cy.get("[data-test='poll-add-button']").click()
    cy.get("[poll-add-test='option-add']").click()
    cy.get("[poll-add-test='option-input-list']").children().should("have.length", 1);
    cy.get("[poll-add-test='option-add']").click()
    cy.get("[poll-add-test='option-input-list']").children().should("have.length", 2);
  })

  // it('closes the create poll component when submit is clicked', () => {
  //   cy.mount(<AddPollComponent updatePolls={() =>{}}/>)
  //   cy.intercept('POST', 'http://localhost:5295/poll', {
  //     statusCode: 201,
  //     body:{}
  //   })

  //   cy.get("[data-test='poll-add-button']").click()
  //   cy.get("[poll-add-test='option-add']").click()
  //   cy.get("[poll-add-test='submit-poll']").click()
  //   cy.get("[data-test='create-new-poll']").should("not.exist");
  // })

  it('calls updatePolls when submit is clicked', () => {

    const spy = {
      updatePolls(){}
    }
    cy.spy(spy, "updatePolls").as('updatePoll')
    cy.mount(<AddPollComponent updatePolls={() => spy.updatePolls()} />)
    cy.intercept('POST', 'http://localhost:5295/poll', {
      statusCode: 201,
      body:{}
    })

    cy.get("[data-test='poll-add-button']").click()
    cy.get("[poll-add-test='option-add']").click()

    cy.get("[poll-add-test='submit-poll']").click()

    cy.get("@updatePoll").should('have.been.called')
  })
})