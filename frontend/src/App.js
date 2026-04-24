import { useCustomerForm } from './hooks/useCustomerForm';
import SignaturePad from './components/SignaturePad';
import { useState } from 'react';

function App() {
  const { form, setForm, errors, handleChange, submit, reset } = useCustomerForm();
  const [signatureKey, setSignatureKey] = useState(0);

  const handleSubmit = async () => {
    try {
      const success = await submit();

      if (success) {
        alert('Customer Registered!');
        reset();
        setSignatureKey((k) => k + 1);
      }
    } catch {
      alert('Error submitting form');
    }
  };

  return (
    <div className="min-h-screen flex items-center justify-center bg-gray-100 p-4">
      <div className="bg-white rounded-2xl shadow-lg p-8 max-w-md w-full">
        <h1 className="text-2xl font-bold text-gray-800 text-center mb-6">
          Customer Onboarding
        </h1>

        <div className="space-y-4">
          <div>
            <label className="block text-sm font-medium text-gray-600 mb-1">
              First Name
            </label>
            <input
              placeholder="First Name"
              value={form.firstName}
              onChange={handleChange('firstName')}
              className={`w-full px-4 py-2 border rounded-lg focus:outline-none focus:ring-2 focus:ring-blue-500 transition duration-200 ${
                errors.firstName ? 'border-red-500' : 'border-gray-300'
              }`}
            />
            {errors.firstName && (
              <p className="text-red-500 text-sm mt-1">{errors.firstName}</p>
            )}
          </div>

          <div>
            <label className="block text-sm font-medium text-gray-600 mb-1">
              Last Name
            </label>
            <input
              placeholder="Last Name"
              value={form.lastName}
              onChange={handleChange('lastName')}
              className={`w-full px-4 py-2 border rounded-lg focus:outline-none focus:ring-2 focus:ring-blue-500 transition duration-200 ${
                errors.lastName ? 'border-red-500' : 'border-gray-300'
              }`}
            />
            {errors.lastName && (
              <p className="text-red-500 text-sm mt-1">{errors.lastName}</p>
            )}
          </div>

          <div>
            <label className="block text-sm font-medium text-gray-600 mb-1">
              Email
            </label>
            <input
              placeholder="Email"
              value={form.email}
              onChange={handleChange('email')}
              className={`w-full px-4 py-2 border rounded-lg focus:outline-none focus:ring-2 focus:ring-blue-500 transition duration-200 ${
                errors.email ? 'border-red-500' : 'border-gray-300'
              }`}
            />
            {errors.email && (
              <p className="text-red-500 text-sm mt-1">{errors.email}</p>
            )}
          </div>

          <div>
            <label className="block text-sm font-medium text-gray-600 mb-1">
              Phone
            </label>
            <input
              placeholder="Phone"
              value={form.phoneNumber}
              onChange={handleChange('phoneNumber')}
              className={`w-full px-4 py-2 border rounded-lg focus:outline-none focus:ring-2 focus:ring-blue-500 transition duration-200 ${
                errors.phoneNumber ? 'border-red-500' : 'border-gray-300'
              }`}
            />
            {errors.phoneNumber && (
              <p className="text-red-500 text-sm mt-1">{errors.phoneNumber}</p>
            )}
          </div>

          <div>
            <label className="block text-sm font-medium text-gray-600 mb-1">
              Signature
            </label>
            <SignaturePad
              key={signatureKey}
              onSave={(sig) => setForm({ ...form, signature: sig })}
              error={errors.signature}
            />
          </div>
        </div>

        <button
          onClick={handleSubmit}
          className="w-full bg-blue-600 text-white py-2 rounded-lg hover:bg-blue-700 transition duration-200 font-medium mt-6"
        >
          Submit
        </button>
      </div>
    </div>
  );
}

export default App;