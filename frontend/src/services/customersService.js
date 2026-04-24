export const createCustomer = async (form) => {
  const response = await fetch('http://localhost:5237/api/customers', {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(form)
  });

  if (!response.ok) {
    throw new Error('Failed to submit');
  }

  return response.json();
};