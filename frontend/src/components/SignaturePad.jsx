import { useRef } from 'react';
import SignatureCanvas from 'react-signature-canvas';

export default function SignaturePad({ onSave, error }) {
  const sigRef = useRef();

  const save = () => {
    if (sigRef.current.isEmpty()) {
      alert('Please provide a signature');
      return;
    }

    onSave(sigRef.current.toDataURL());
  };

  const clear = () => {
    sigRef.current.clear();
    onSave('');
  };

  return (
    <div className="mt-2">
      <div className="border border-gray-300 rounded-lg overflow-hidden bg-gray-50">
        <SignatureCanvas
          ref={sigRef}
          canvasProps={{
            width: 400,
            height: 150,
            className: 'w-full'
          }}
        />
      </div>

      {error && <p className="text-red-500 text-sm mt-1">{error}</p>}

      <div className="flex gap-2 mt-2">
        <button
          onClick={save}
          className="flex-1 bg-gray-200 text-gray-700 py-2 rounded-lg hover:bg-gray-300 transition duration-200 font-medium"
        >
          Save
        </button>
        <button
          onClick={clear}
          className="flex-1 bg-gray-200 text-gray-700 py-2 rounded-lg hover:bg-gray-300 transition duration-200 font-medium"
        >
          Clear
        </button>
      </div>
    </div>
  );
}