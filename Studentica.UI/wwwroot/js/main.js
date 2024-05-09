window.SetFocusToElement = (element) => {
    element.focus();
    element.scrollIntoView({ behavior: 'smooth' });
}