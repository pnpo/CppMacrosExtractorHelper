describe('Default page test suite', () => {

    beforeEach(() => {

        cy.visitPortal()
    })

    it('should contains title', () => {

        cy.contains('🔥 cx web kickstart 🔥')
    })

    it('should contains cx-ui button', () => {

        cy.get('button')
    })
})