import { useState } from 'react';
import { validateCustomer } from '../utils/validation';
import { createCustomer } from '../services/customersService';
import { formatPhone, cleanPhone, toLowerCase } from '../utils/formatters';

export const useCustomerForm = () => {
  const [form, setForm] = useState({
    firstName: '',
    lastName: '',
    email: '',
    phoneNumber: '',
    signature: ''
  });

  const [errors, setErrors] = useState({});

  const handleChange = (field) => (e) => {
    let value = e.target.value;

    if (field === 'phoneNumber') {
      value = formatPhone(value);
    } else if (field === 'email') {
      value = toLowerCase(value);
    }

    setForm({ ...form, [field]: value });

    if (errors[field]) {
      setErrors({ ...errors, [field]: null });
    }
  };

  const submit = async () => {
    const formData = {
      ...form,
      phoneNumber: cleanPhone(form.phoneNumber)
    };

    const validationErrors = validateCustomer(formData);

    if (Object.keys(validationErrors).length > 0) {
      setErrors(validationErrors);
      return false;
    }

    await createCustomer(formData);
    return true;
  };

  const reset = () => {
    setForm({
      firstName: '',
      lastName: '',
      email: '',
      phoneNumber: '',
      signature: ''
    });
    setErrors({});
  };

  return {
    form,
    setForm,
    errors,
    setErrors,
    handleChange,
    submit,
    reset
  };
};