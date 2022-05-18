describe('login_time_test', () => {
    it('visits mediahub, loggs into an existing account and measures the time needed', () => {
        cy.visit('https://localhost:7080');
        cy.get('#Input_Email').type('adrian.locher@ost.ch');
        cy.get('#Input_Password').type('$Adrian6');
        cy.get('#login-submit').click();
        cy.get('.userProfileOverview').should('be.visible');
    });
}); 