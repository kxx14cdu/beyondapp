using Android.Text;
using Android.Text.Style;
using System;
using System.Collections.Generic;
using bfbnet;
using bfbnet.Droid;
using Org.Xml.Sax;

namespace Android.Text
{
	public class HtmlTagHandler : Java.Lang.Object, Android.Text.Html.ITagHandler {
		/**
         * Keeps track of lists (ol, ul). On bottom of Stack is the outermost list
         * and on top of Stack is the most nested list
         */
		Stack<String> lists = new Stack<String>();
		/**
         * Tracks indexes of ordered lists so that after a nested list ends
         * we can continue with correct index of outer list
         */
		Stack<Int32> olNextIndex = new Stack<Int32>();
		/**
         * List indentation in pixels. Nested lists use multiple of this.
         */
		private static  int indent = 10;
		private static  int listItemIndent = indent * 2;
		private static  BulletSpan bullet = new BulletSpan(indent);

		private  class Ul : Java.Lang.Object {
		}

		private  class Ol : Java.Lang.Object {
		}

		private  class Code : Java.Lang.Object {
		}

		private  class Center : Java.Lang.Object {
		}

		private  class Strike : Java.Lang.Object {
		}


		public void HandleTag( Boolean opening, String tag, Android.Text.IEditable output,  IXMLReader xmlReader) {
			if (opening) {
				// opening tag
				//          if (HtmlTextView.DEBUG) {
				//              Log.d(HtmlTextView.TAG, "opening, output: " + output.ToString());
				//          }
				//
				if (tag.ToLower() == "ul") {
					lists.Push(tag);
				} else if (tag.Equals("ol")) {
					lists.Push(tag);
					olNextIndex.Push(1);
				} else if (tag.Equals("li")) {
					if (output.Length() > 0 && output.CharAt(output.Length() - 1) != '\n') {
						output.Append("\n");
					}
					String parentList = lists.Peek();
					if (parentList.Equals("ol")) {
						start(output, new Ol());
						output.Append(olNextIndex.Peek().ToString()).Append('.').Append(' ');
						olNextIndex.Push(olNextIndex.Pop() + 1);
					} else if (parentList.Equals("ul")) {
						start(output, new Ul());
					}
				} else if (tag.Equals("code")) {
					start(output, new Code());
				} else if (tag.Equals("center")) {
					start(output, new Center());
				} else if (tag.Equals("s") || tag.Equals("strike")) {
					start(output, new Strike());
				}
			} else {
				// closing tag
				//          if (HtmlTextView.DEBUG) {
				//              Log.d(HtmlTextView.TAG, "closing, output: " + output.ToString());
				//          }
				//
				if (tag.Equals("ul")) {
					lists.Pop();
				} else if (tag.Equals("ol")) {
					lists.Pop();
					olNextIndex.Pop();
				} else if (tag.Equals("li")) {
					if (lists.Peek().Equals("ul")) {
						if (output.Length() > 0 && output.CharAt(output.Length() - 1) != '\n') {
							output.Append("\n");
						}
						// Nested BulletSpans increases distance between bullet and Text, so we must prevent it.
						int bulletMargin = indent;
						if (lists.Count > 1) {
							bulletMargin = indent - bullet.GetLeadingMargin(true);
							if (lists.Count > 2) {
								// This get's more complicated when we add a LeadingMarginSpan into the same line:
								// we have also counter it's effect to BulletSpan
								bulletMargin -= (lists.Count - 2) * listItemIndent;
							}
						}
						BulletSpan newBullet = new BulletSpan(bulletMargin);
						end(output, typeof(Ul), false,
							new LeadingMarginSpanStandard(listItemIndent * (lists.Count - 1)),
							newBullet);
					} else if (lists.Peek().Equals("ol")) {
						if (output.Length() > 0 && output.CharAt(output.Length() - 1) != '\n') {
							output.Append("\n");
						}
						int numberMargin = listItemIndent * (lists.Count - 1);
						if (lists.Count > 2) {
							// Same as in ordered lists: counter the effect of nested Spans
							numberMargin -= (lists.Count - 2) * listItemIndent;
						}
						end(output, typeof(Ol), false, new LeadingMarginSpanStandard(numberMargin));
					}
				} else if (tag.Equals("code")) {
					end(output, typeof(Code), false, new TypefaceSpan("monospace"));
				} else if (tag.Equals("center")) {
					end(output, typeof(Center), true, new AlignmentSpanStandard(Layout.Alignment.AlignCenter));
				} else if (tag.Equals("s") || tag.Equals("strike")) {
					end(output, typeof(Strike), false, new StrikethroughSpan());
				}
			}
		}

		/**
         * Mark the opening tag by using private classes
         */
		private void start(IEditable output, Java.Lang.Object mark) {
			int len = output.Length();
			output.SetSpan(mark, len, len, SpanTypes.MarkMark);

			//      if (HtmlTextView.DEBUG) {
			//          Log.d(HtmlTextView.TAG, "len: " + len);
			//      }
		}

		/**
         * Modified from {@link Android.Text.Html}
         */
		private void end(IEditable output, Type kind, Boolean paragraphStyle, params Java.Lang.Object[] replaces) {
			Java.Lang.Object obj = getLast(output, kind);
			// start of the tag
			int where = output.GetSpanStart(obj);
			// end of the tag
			int len = output.Length();

			output.RemoveSpan(obj);

			if (where != len) {
				int thisLen = len;
				// paragraph styles like AlignmentSpan need to end with a new line!
				if (paragraphStyle) {
					output.Append("\n");
					thisLen++;
				}
				foreach (Java.Lang.Object replace in replaces) {
					output.SetSpan(replace, where, thisLen, SpanTypes.ExclusiveExclusive);
				}

				//          if (HtmlTextView.DEBUG) {
				//              Log.d(HtmlTextView.TAG, "where: " + where);
				//              Log.d(HtmlTextView.TAG, "thisLen: " + thisLen);
				//          }
			}
		}

		/**
         * Get last marked position of a specific tag kind (private class)
         */
		private static Java.Lang.Object getLast(IEditable Text, Type kind) {
			Java.Lang.Object[] objs = Text.GetSpans(0, Text.Length(), Java.Lang.Class.FromType(kind)); // TODO: LOl will this work?
			if (objs.Length == 0) {
				return null;
			} else {
				for (int i = objs.Length; i > 0; i--) {
					if (Text.GetSpanFlags(objs[i - 1]) == SpanTypes.MarkMark) {
						return objs[i - 1];
					}
				}
				return null;
			}
		}

	} 
}