export const formatPhone = (value) => {
  const digits = value.replace(/\D/g, '').slice(0, 11);

  if (digits.length <= 4) return digits;
  if (digits.length <= 7) return `${digits.slice(0, 4)} ${digits.slice(4)}`;
  return `${digits.slice(0, 4)} ${digits.slice(4, 7)} ${digits.slice(7)}`;
};

export const cleanPhone = (value) => {
  return value.replace(/\D/g, '');
};

export const toLowerCase = (value) => {
  return value.trim().toLowerCase();
};