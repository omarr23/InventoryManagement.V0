import React from 'react';

interface SkeletonProps {
  className?: string;
}

export const Skeleton: React.FC<SkeletonProps> = ({ className = '' }) => {
  return (
    <div className={`animate-pulse bg-gray-200 rounded ${className}`} />
  );
};

export const MetricSkeleton: React.FC = () => {
  return (
    <div className="bg-white p-4 rounded-lg shadow">
      <Skeleton className="h-8 w-16 mb-2" />
      <Skeleton className="h-4 w-24" />
    </div>
  );
};

export const ProductDetailsSkeleton: React.FC = () => {
  return (
    <div className="bg-white p-5 rounded-lg shadow">
      <Skeleton className="h-6 w-32 mb-4" />
      <div className="flex justify-between">
        <div className="w-1/2">
          {[1, 2, 3, 4].map((i) => (
            <div key={i} className="flex justify-between py-2">
              <Skeleton className="h-4 w-24" />
              <Skeleton className="h-4 w-8" />
            </div>
          ))}
        </div>
        <div className="w-1/2 flex justify-center items-center">
          <Skeleton className="w-32 h-32 rounded-full" />
        </div>
      </div>
    </div>
  );
};

export const TopSellingItemsSkeleton: React.FC = () => {
  return (
    <div className="bg-white p-5 rounded-lg shadow">
      <Skeleton className="h-6 w-32 mb-4" />
      <div className="grid grid-cols-2 gap-4">
        {[1, 2].map((i) => (
          <div key={i} className="bg-gray-50 p-4 rounded-lg text-center">
            <Skeleton className="w-20 h-20 rounded-lg mx-auto mb-2" />
            <Skeleton className="h-5 w-32 mx-auto mb-2" />
            <Skeleton className="h-6 w-16 mx-auto mb-1" />
            <Skeleton className="h-4 w-12 mx-auto" />
          </div>
        ))}
      </div>
    </div>
  );
};

export const OrderPanelSkeleton: React.FC = () => {
  return (
    <div className="bg-white p-5 rounded-lg shadow">
      <Skeleton className="h-6 w-32 mb-4" />
      <Skeleton className="h-8 w-24 mb-2" />
      <Skeleton className="h-10 w-32" />
    </div>
  );
}; 