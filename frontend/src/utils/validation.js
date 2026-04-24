const EMAIL_REGEX = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

const isValidPhilippinePhone = (phone) => {
  const digits = phone.replace(/\D/g, '');
  return digits.length === 11 && digits.startsWith('09');
};

export const validateCustomer = (form) => {
  const errors = {};

  if (!form.firstName.trim()) errors.firstName = 'First Name is required';
  if (!form.lastName.trim()) errors.lastName = 'Last Name is required';

  if (!form.email.trim()) {
    errors.email = 'Email is required';
  } else if (!EMAIL_REGEX.test(form.email)) {
    errors.email = 'Invalid email format';
  }

  if (!form.phoneNumber.trim()) {
    errors.phoneNumber = 'Phone Number is required';
  } else if (!isValidPhilippinePhone(form.phoneNumber)) {
    errors.phoneNumber = 'Invalid phone number';
  }

  if (!form.signature) errors.signature = 'Signature is required';

  return errors;
};