package recyclerview.sriyank.com.recyclerview.adapter;

import android.content.Context;
import android.location.Location;
import android.support.v7.widget.RecyclerView;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.TextView;

import java.util.List;

import recyclerview.sriyank.com.recyclerview.R;
import recyclerview.sriyank.com.recyclerview.model.Landscape;

public class RecyclerAdapter extends RecyclerView.Adapter<RecyclerAdapter.MyViewHolder> {
	List<Landscape> mData;
	private LayoutInflater inflater;

	public RecyclerAdapter(Context context, List<Landscape> data) {
		inflater = LayoutInflater.from(context);
		this.mData = data;
	}

	//вызывается вместе с onBindViewHolder, а потом, если много элементов возвращающийся MyViewHolder
	//просто переиспользуется и вызывается в адаптере только onBindViewHolder
	@Override
	public MyViewHolder onCreateViewHolder(ViewGroup parent, int viewType) {
		View view = inflater.inflate(R.layout.list_item, parent, false);
		MyViewHolder holder = new MyViewHolder(view);
		return holder;
	}

	//call during scroll down or up and therefore items will be added not all at once but in portions
	@Override
	public void onBindViewHolder(MyViewHolder holder, int position) {
		Landscape current = mData.get(position);
		holder.setData(current, position);
	}

	@Override
	public int getItemCount() {
		return mData.size();
	}

	class MyViewHolder extends RecyclerView.ViewHolder  {
		TextView title;
		ImageView imgThumb, imgDelete, imgAdd;
		int position;
		Landscape current;

		public MyViewHolder(View itemView) {
			super(itemView);
			title       = (TextView)  itemView.findViewById(R.id.tvTitle);
			imgThumb    = (ImageView) itemView.findViewById(R.id.img_row);
			imgDelete   = (ImageView) itemView.findViewById(R.id.img_row_delete);
			imgAdd      = (ImageView) itemView.findViewById(R.id.img_row_add);
			imgDelete.setOnClickListener(new View.OnClickListener() {
				@Override
				public void onClick(View view) {
					Log.i("Delete",title.getText().toString());
					Landscape l = mData.get(position);
					mData.remove(l);
					notifyDataSetChanged();
				}
			});
		}

		public void setData(Landscape current, int position) {
			this.title.setText(current.getTitle());
			this.imgThumb.setImageResource(current.getImageID());
			this.position = position;
			this.current = current;
		}

	}
}
