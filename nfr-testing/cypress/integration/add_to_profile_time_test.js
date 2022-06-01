describe('login_time_test', () => {
    it('visits mediahub, loggs into an existing account and measures the time needed', () => {
        cy.visit('https://localhost:7080');
        cy.get('#Input_Email').type('test@email.com');
        cy.get('#Input_Password').type('$TestPwd111');
        cy.get('#login-submit').click();

        
        cy.wait(1000);
        cy.get('.navbar-toggler-icon').click();
        cy.wait(1000);
        cy.get('[href="mediaSearch"]').click();
        cy.wait(1000);
        cy.get('[type="search"]').type("harry potter");
        cy.wait(1000);
        cy.get('.btn-primary').click();
        cy.wait(1000);
        cy.get('tbody > tr:first-child img').click();
        cy.wait(1000);
        cy.get('.btn-primary').click();
        
    });
}); 
